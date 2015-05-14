using System;
using System.Linq;
using ATM.Logic.Cards.Enums;
using ATM.Logic.Cards.OperationResults;

namespace ATM.Logic.Cards
{
  public class CardOperations
  {
    public GetCardIdResult GetCardId(string cardNumber)
    {
      var result = new GetCardIdResult();

      using (var db = new ATMEntities())
      {
        var selectCard = from c in db.Cards where c.Number == cardNumber select c;
        var card = selectCard.FirstOrDefault();

        if (card == null)
        {
          result.Status = OperationStatus.UnknownCard;
        }
        else if (card.BlockReason != null)
        {
          result.Status = OperationStatus.BlockedCard;
        }
        else
        {
          result.Status = OperationStatus.Succeeded;
          result.CardID = card.ID;
        }
      }

      return result;
    }

    public CheckPinCodeResult CheckPinCode(long cardID, string pinCode, int maxFailedLogins)
    {
      using (var db = new ATMEntities())
      {
        db.Database.Log = Console.WriteLine;

        var result = new CheckPinCodeResult();

        using (var transaction = db.Database.BeginTransaction())
        {
          db.Database.ExecuteSqlCommand(@"
            UPDATE
              Cards
            SET
              FailedLogins = CASE WHEN PinCode <> {0} THEN FailedLogins + 1 ELSE 0 END,
              BlockReason = CASE WHEN PinCode <> {0} AND FailedLogins >= {1} THEN {3} END
            WHERE
              ID = {2}
            AND
              BlockReason IS NULL;
            ",
            pinCode,
            maxFailedLogins,
            cardID,
            CardBlockReason.TooManyFailedLogins
          );

          var card = GetCardByID(db, cardID, result);

          transaction.Commit();

          if (result.Status == OperationStatus.Succeeded)
          {
            if (card.FailedLogins != 0)
            {
              result.Status = OperationStatus.InvalidPinCode;
            }
          }
        }

        return result;
      }
    }

    public GetBalanceResult GetBalance(long cardID)
    {
      var result = new GetBalanceResult();

      using (var db = new ATMEntities())
      {
        db.Database.Log = Console.WriteLine;

        var card = GetCardByID(db, cardID, result);

        if (result.Status == OperationStatus.Succeeded)
        {
          var operation = new Operation
          {
            CardID = cardID,
            Code = OperationCode.GetBalance
          };

          db.Operations.Add(operation);
          db.SaveChanges();

          result.CardNumber = card.Number;
          result.Balance = card.Balance;
          result.CurrencyCode = card.CurrencyCode;
          result.Date = DateTime.SpecifyKind(operation.Date, DateTimeKind.Utc);
        }
      }

      return result;
    }


    public WithdrawalResult Withdraw(long cardID, decimal amount)
    {
      using (var db = new ATMEntities())
      {
        db.Database.Log = Console.WriteLine;

        var result = new WithdrawalResult();

        // An amount sdhould not be negative
        if (amount <= 0)
        {
          result.Status = OperationStatus.InvalidAmount;
          return result;
        }

        using (var transaction = db.Database.BeginTransaction())
        {
          // The native update here is to avoid concurency issues.
          var rowsAffected = db.Database.ExecuteSqlCommand(@"
            UPDATE
              Cards
            SET
              Balance = Balance - {0}
            WHERE
              ID = {1}
            AND
              Balance >= {0}
            AND
              BlockReason IS NULL
            ",
            amount,
            cardID
          );

          var card = GetCardByID(db, cardID, result);

          if (rowsAffected == 0)
          {
            if (result.Status == OperationStatus.Succeeded)
            {
              result.Status = OperationStatus.InsufficientFunds;
            }
          }
          else
          {
            var operation = new Operation
            {
              CardID = cardID,
              Code = OperationCode.Withdraw,
              Amount = amount
            };

            db.Operations.Add(operation);
            db.SaveChanges();
            transaction.Commit();

            result.Status = OperationStatus.Succeeded;
            result.CardNumber = card.Number;
            result.Amount = operation.Amount.Value;
            result.Balance = card.Balance;
            result.CurrencyCode = card.CurrencyCode;
            result.Date = DateTime.SpecifyKind(operation.Date, DateTimeKind.Utc);
          }
        }

        return result;
      }
    }

    protected Card GetCardByID(ATMEntities db, long cardID, CardOperationResult result = null)
    {
      var query = from c in db.Cards where c.ID == cardID select c;
      var card = query.SingleOrDefault();

      if (result != null)
      {
        if (card == null)
        {
          result.Status = OperationStatus.UnknownCard;
        }
        else if (card.BlockReason != null)
        {
          result.Status = OperationStatus.BlockedCard;
        }
        else
        {
          result.Status = OperationStatus.Succeeded;
        }
      }

      return card;
    }
  }
}

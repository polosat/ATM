using System;

namespace ATM.Logic.Cards.OperationResults
{
  public class GetBalanceResult : CardOperationResult
  {
    public string CardNumber { get; set; }
    public DateTime Date { get; set; }
    public decimal Balance { get; set; }
    public short CurrencyCode { get; set; }
  }
}

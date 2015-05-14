using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Configuration;
using System.Security.Claims;
using System.Security.Principal;
using ATM.Logic.Cards;
using ATM.Logic.Cards.Enums;
using ATM.Web.Models.Card;
using ATM.Web.Infrastructure.Common;
using ATM.Web.Infrastructure.Security;

namespace ATM.Web.Controllers
{
  public class CardController : ApiController
  {
    [HttpPost]
    [AllowAnonymous]
    public void Identify(IdentifyCardRequest request)
    {
      CardOperations cards = new CardOperations();
      var result = cards.GetCardId(request.CardNumber);

      if (result.Status != OperationStatus.Succeeded)
        throw new HttpResponseException(HttpStatusCode.Unauthorized);

      var authentication = new AuthenticationProvider(Request.GetOwinContext());
      authentication.Identify(result.CardID);
    }

    [HttpPost]
    [AllowAnonymous]
    public void Authenticate(AuthenticateCardRequest request)
    {
      if (!RequestContext.Principal.Identity.IsAuthenticated)
        throw new HttpResponseException(HttpStatusCode.Unauthorized);

      var maxFailedLogins = Int32.Parse(WebConfigurationManager.AppSettings["card:MaxFailedLogins"]);

      CardOperations cards = new CardOperations();
      var result = cards.CheckPinCode(CardID, request.PinCode, maxFailedLogins);

      if (result.Status == OperationStatus.InvalidPinCode)
        throw new HttpResponseException(Request.CreateLogicErrorResponse("Incorrect PIN code"));

      var authentication = new AuthenticationProvider(Request.GetOwinContext());

      if (result.Status != OperationStatus.Succeeded)
      {
        authentication.SignOut();
        throw new HttpResponseException(HttpStatusCode.Unauthorized);
      }
        
      authentication.Authenticate(RequestContext.Principal.Identity as ClaimsIdentity);
    }

    [HttpPost]
    [AllowAnonymous]
    public void Logout()
    {
      var authentication = new AuthenticationProvider(Request.GetOwinContext());
      authentication.SignOut();
    }

    [HttpPost]
    public GetBalanceResponse GetBalance()
    {
      CardOperations cards = new CardOperations();
      var result = cards.GetBalance(CardID);

      if (result.Status != OperationStatus.Succeeded)
        throw new HttpResponseException(HttpStatusCode.Unauthorized);

      var response = new GetBalanceResponse
      {
        CardNumber = result.CardNumber,
        Balance = result.Balance,
        Date = result.Date,
        CurrencyCode = result.CurrencyCode
      };

      return response;
    }

    [HttpPost]
    public WithdrawalResponse Withdraw(WithdrawalRequest request)
    {
      CardOperations cards = new CardOperations();
      var result = cards.Withdraw(CardID, request.Amount);

      if (result.Status == OperationStatus.InsufficientFunds)
        throw new HttpResponseException(Request.CreateLogicErrorResponse("Insufficient funds"));

      if (result.Status == OperationStatus.InvalidAmount)
        throw new HttpResponseException(HttpStatusCode.BadRequest);

      if (result.Status != OperationStatus.Succeeded)
        throw new HttpResponseException(HttpStatusCode.Unauthorized);

      var response = new WithdrawalResponse
      {
        CardNumber = result.CardNumber,
        Date = result.Date,
        Amount = result.Amount,
        Balance = result.Balance,
        CurrencyCode = result.CurrencyCode
      };

      return response;
    }

    protected long CardID
    {
      get
      {
        IIdentity identity = RequestContext.Principal.Identity;
        if (!identity.IsAuthenticated)
          throw new HttpResponseException(HttpStatusCode.Unauthorized);

        return Int64.Parse(identity.Name);
      }
    }
  }
}

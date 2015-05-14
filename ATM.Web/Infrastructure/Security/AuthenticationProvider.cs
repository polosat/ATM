using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;

namespace ATM.Web.Infrastructure.Security
{
  public class AuthenticationProvider
  {
    protected IOwinContext owinContext;
    public AuthenticationProvider(IOwinContext context)
    {
      if (context == null)
        throw new ArgumentNullException("context");

      owinContext = context;      
    }

    public void Identify(long cardID)
    {
      var claims = new List<Claim>();
      claims.Add(new Claim(ClaimTypes.Name, cardID.ToString()));

      var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
      var authentication = owinContext.Authentication;
      authentication.SignIn(identity);
    }

    public void Authenticate(ClaimsIdentity identity)
    {
      if (identity != null && identity.IsAuthenticated)
      {
        identity.AddClaim(new Claim(ClaimTypes.Role, "Verified"));
        var authentication = owinContext.Authentication;
        authentication.SignIn(identity);
      }
    }

    public void SignOut()
    {
      var authentication = owinContext.Authentication;
      authentication.SignOut();
    }
  }
}
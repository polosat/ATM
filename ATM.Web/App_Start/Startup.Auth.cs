using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ATM.Web
{
  public partial class Startup
  {
    public void ConfigureAuth(IAppBuilder app)
    {
      var expireTimeSpan = Int32.Parse(WebConfigurationManager.AppSettings["auth:ExpireTimeSpan"]);
      var slidingExpiration = Boolean.Parse(WebConfigurationManager.AppSettings["auth:SlidingExpiration"]);

      app.UseCookieAuthentication(new CookieAuthenticationOptions {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        ExpireTimeSpan = TimeSpan.FromSeconds(expireTimeSpan),
        SlidingExpiration = slidingExpiration
      });
    }
  }
}
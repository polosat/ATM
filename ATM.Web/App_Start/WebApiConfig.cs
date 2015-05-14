using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ATM.Web.Infrastructure.Validation;

namespace ATM.Web
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{Action}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );

      config.Filters.Add(new AuthorizeAttribute { Roles = "Verified" });
      config.Filters.Add(new ValidationActionFilter());

    }
  }
}

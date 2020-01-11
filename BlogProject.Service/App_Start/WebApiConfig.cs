using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BlogProject.Service.Models;
namespace BlogProject.Service
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
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.MessageHandlers.Add(new AuthenticationMessageHandler());
            config.MessageHandlers.Add(new ExceptionMessageHandler());
            config.Filters.Add(new DbExceptionFilterAttribute());
            config.Filters.Add(new AuthorizeAttribute());
            config.DependencyResolver = new DependencyResolver();
        }
    }
}

using Microsoft.Practices.Unity;
using refactor_me.Repositories;
using System.Web.Http;
using UnityLog4NetExtension.Log4Net;

namespace refactor_me
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Configuring the Dependency Resolver
            var container = new UnityContainer();
            container.RegisterType<IProducts, Products>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductOptions, ProductOptions>(new HierarchicalLifetimeManager());
            container.AddNewExtension<Log4NetExtension>();
            config.DependencyResolver = new UnityResolver(container);

            //Logging for API with log4net
            log4net.Config.XmlConfigurator.Configure();

            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.Indent = true;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
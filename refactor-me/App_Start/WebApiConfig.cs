using Microsoft.Practices.Unity;
using refactor_me.Repositories;
using System.Web.Http;

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
            config.DependencyResolver = new UnityResolver(container);

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
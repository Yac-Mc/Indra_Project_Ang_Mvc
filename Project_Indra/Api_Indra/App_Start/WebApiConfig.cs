using BusinessLogic_Indra.User;
using DataAccess_Indra.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.WebApi;

namespace Api_Indra
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            AddDependencies(config);
            // Web API configuration and services
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void AddDependencies(HttpConfiguration config)
        {
            // Registro de componentes para inyección de dependencias
            var container = new UnityContainer();
            // BusinessLogic
            container.RegisterType<IUserBL, UserBL>(new HierarchicalLifetimeManager());
            // Repository
            container.RegisterType<IUserRepository, UserRepository>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}

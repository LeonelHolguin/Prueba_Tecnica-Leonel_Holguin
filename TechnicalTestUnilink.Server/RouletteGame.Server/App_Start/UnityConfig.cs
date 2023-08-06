using RouletteGame.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace RouletteGame.Server.App_Start
{
    public class UnityConfig
    {
        internal static readonly IUnityContainer Container;

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<TechnicalTestUnilink_DBContext>(new PerRequestLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
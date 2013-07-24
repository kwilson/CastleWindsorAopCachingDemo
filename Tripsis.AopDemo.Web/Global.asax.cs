using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tripsis.AopDemo.Web
{
    using System.IO;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    using Tripsis.AopDemo.Web.Plumbing;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BootstrapContainer();
        }

        /// <summary>
        /// Bootstraps the Castle container.
        /// </summary>
        private static void BootstrapContainer()
        {
            var container = new WindsorContainer()
                .Install(FromAssembly.This())
                .Install(GetInstallersFromWildcard("*.Installers.dll").ToArray());

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        /// <summary>
        /// Gets the <see cref="IWindsorInstaller"/> from the current executing directory which match 
        /// the specified <param name="wildcard"></param>.
        /// </summary>
        /// <param name="wildcard">The wildcard to match.</param>
        /// <returns>A collection of <see cref="IWindsorInstaller"/>s.</returns>
        private static IEnumerable<IWindsorInstaller> GetInstallersFromWildcard(string wildcard)
        {
            var binDirectory = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
            if (Directory.Exists(binDirectory))
            {
                var files = Directory.GetFiles(binDirectory, wildcard, SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    yield return FromAssembly.Named(file);
                }
            }
        }
    }
}
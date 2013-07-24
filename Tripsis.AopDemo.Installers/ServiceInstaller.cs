namespace Tripsis.AopDemo.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using Tripsis.AopDemo.Caching;
    using Tripsis.AopDemo.Caching.WebCache;
    using Tripsis.AopDemo.Services;
    using Tripsis.AopDemo.Services.Linq;

    public class ServiceInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<CacheInterceptor>().LifestyleTransient().DependsOn(Dependency.OnAppSettingsValue("cacheTimeoutSeconds"), Dependency.OnAppSettingsValue("cachingIsEnabled")),
                Component.For<ICacheProvider>().ImplementedBy<WebCacheProvider>().LifestyleSingleton(),
                Component.For<IStockServices>().ImplementedBy<StockServices>().LifestylePerWebRequest().Interceptors<CacheInterceptor>());
        }
    }
}

using System.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;
using SignalRPoc.App_Data;
using SignalRPoc.Filters;
using Ninject.Web.WebApi;
using System.Web.Http;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SignalRPoc.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(SignalRPoc.App_Start.NinjectWebCommon), "Stop")]

namespace SignalRPoc.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Microsoft.AspNet.SignalR;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                //Set the dependency resolver for Web API
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                //Set the dependency resolver for SignalR
                GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILockStore>().To<LockStore>();

            kernel
                .BindFilter<TakesALockFilter>(FilterScope.Action, 0)
                .WhenActionMethodHas<TakesALockAttribute>();

            kernel
                .BindFilter<ReleasesALockFilter>(FilterScope.Action, 0)
                .WhenActionMethodHas<ReleasesALockAttribute>();
        }        
    }
}

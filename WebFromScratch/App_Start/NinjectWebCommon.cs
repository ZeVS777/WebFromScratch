using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc;
using WebFromScratch;
using WebFromScratch.Infrastructure.NinjectModules;

// ����� Start ������ NinjectWebCommon ����� ������� ����� ������� Application_Start � Global.asax.cs
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
// ����� Stop ������ NinjectWebCommon ����� ������� ����� ������ Application_End � Global.asax.cs
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace WebFromScratch
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// ������ ����������
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// ��������� ����������.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// ������ ����, ������������� ����������.
        /// </summary>
        /// <returns>��������� ����.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// ���������� ������ � ������������ �������.
        /// </summary>
        /// <param name="kernel">����.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<UrlHelper>().ToMethod((e) =>
            {
                var context = new HttpContextWrapper(HttpContext.Current);
                var routeData = RouteTable.Routes.GetRouteData(context);
                return new UrlHelper(new RequestContext(context, routeData));
            }).InRequestScope();
            kernel.Load<ServicesModule>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }        
    }
}

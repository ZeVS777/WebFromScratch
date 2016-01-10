using Ninject.Modules;
using Ninject.Web.Common;
using WebFromScratch.Services;

namespace WebFromScratch.Infrastructure.NinjectModules
{
    public class ServicesModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IManifestService>().To<ManifestService>().InRequestScope();
            Bind<IBrowserConfigService>().To<BrowserConfigService>().InRequestScope();
        }
    }
}
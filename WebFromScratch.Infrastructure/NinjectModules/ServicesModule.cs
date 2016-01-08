using Ninject.Modules;
using Ninject.Web.Common;
using WebFromScratch.Services.ManifestService;

namespace WebFromScratch.Infrastructure.NinjectModules
{
    public class ServicesModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IManifestService>().To<ManifestService>().InRequestScope();
        }
    }
}
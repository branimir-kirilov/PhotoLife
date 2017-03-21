using Ninject.Modules;
using PhotoLife.Providers;
using PhotoLife.Providers.Contracts;

namespace PhotoLife.App_Start.NinjectModules
{
    public class ProvidersNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IHttpContextProvider>().To<HttpContextProvider>();
            this.Bind<IDateTimeProvider>().To<DateTimeProvider>();
        }
    }
}
using Ninject.Modules;
using PhotoLife.Authentication.Providers;

namespace PhotoLife.App_Start.NinjectModules
{
    public class AuthenticationNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IAuthenticationProvider>().To<HttpContextAuthenticationProvider>();
        }
    }
}
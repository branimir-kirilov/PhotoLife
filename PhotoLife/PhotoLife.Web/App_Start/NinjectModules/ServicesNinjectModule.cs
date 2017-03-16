using Ninject.Modules;
using PhotoLife.Services;
using PhotoLife.Services.Contracts;

namespace PhotoLife.App_Start.NinjectModules
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUserService>().To<UserService>();
        }
    }
}
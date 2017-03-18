using Ninject.Extensions.Factory;
using Ninject.Modules;
using PhotoLife.Factories;

namespace PhotoLife.App_Start.NinjectModules
{
    public class FactoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ICommentFactory>().ToFactory().InSingletonScope();
            this.Bind<IUserFactory>().ToFactory().InSingletonScope();
            this.Bind<INewsFactory>().ToFactory().InSingletonScope();
            this.Bind<IPostFactory>().ToFactory().InSingletonScope();
            this.Bind<ICategoryFactory>().ToFactory().InSingletonScope();

            this.Bind<IViewModelFactory>().ToFactory().InSingletonScope();
        }
    }
}
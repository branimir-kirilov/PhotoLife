using CloudinaryDotNet;
using Ninject.Activation;
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

            this.Bind<Cloudinary>()
                .ToMethod(this.Cloudinary)
                .NamedLikeFactoryMethod((ICloudinaryFactory f) => f.Cloudinary());

        }

        private Cloudinary Cloudinary(IContext args)
        {
            var acc = new Account(
            Properties.Settings.Default.CloudName,
            Properties.Settings.Default.CloudinaryApiKey,
            Properties.Settings.Default.CloudinaryApiSecret);

            return new Cloudinary(acc);
        }
    }
}
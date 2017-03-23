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
            //Models factories
            this.Bind<ICommentFactory>().ToFactory().InSingletonScope();
            this.Bind<IUserFactory>().ToFactory().InSingletonScope();
            this.Bind<INewsFactory>().ToFactory().InSingletonScope();
            this.Bind<IPostFactory>().ToFactory().InSingletonScope();
            this.Bind<ICategoryFactory>().ToFactory().InSingletonScope();

            //View models
            this.Bind<IViewModelFactory>().ToFactory().InSingletonScope();

            //Cloudinary service
            this.Bind<Cloudinary>()
                .ToMethod(this.GetCloudinary);

        }

        //Configuring Cloudinary Account
        private Cloudinary GetCloudinary(IContext args)
        {
            var acc = new Account(
            Properties.Settings.Default.CloudName,
            Properties.Settings.Default.CloudinaryApiKey,
            Properties.Settings.Default.CloudinaryApiSecret);

            return new Cloudinary(acc);
        }
    }
}
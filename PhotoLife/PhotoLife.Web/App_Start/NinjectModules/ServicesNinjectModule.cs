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
            this.Bind<IPostService>().To<PostsService>();
            this.Bind<INewsService>().To<NewsService>();
            this.Bind<ICategoryService>().To<CategoryService>();
            this.Bind<ICommentService>().To<CommentService>();
        }
    }
}
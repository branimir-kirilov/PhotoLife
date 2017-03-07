using Ninject.Modules;
using PhotoLife.Data.Contracts;
using Ninject.Web.Common;
using PhotoLife.Data;

namespace PhotoLife.App_Start.NinjectModules
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IPhotoLifeEntities>().To<PhotoLifeEntities>().InRequestScope();
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            this.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
        }
    }
}
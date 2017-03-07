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
            Kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            Kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
        }
    }
}
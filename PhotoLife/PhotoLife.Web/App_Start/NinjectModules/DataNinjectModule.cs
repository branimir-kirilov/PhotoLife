using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using PhotoLife.Data.Contracts;
using Ninject.Extensions.Conventions;
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
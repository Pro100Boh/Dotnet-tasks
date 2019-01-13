using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.RepositoriesEF;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class CatalogModule : NinjectModule
    {

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument("DbConnection");
            //Bind(typeof(IUnitOfWork)).To(typeof(ADOUnitOfWork)).WithConstructorArgument("DefaultConnection");
        }
    }
}

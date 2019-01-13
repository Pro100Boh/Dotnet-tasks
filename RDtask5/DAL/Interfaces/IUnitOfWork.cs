using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Supplier> Suppliers { get; }

        IRepository<Product> Products { get; }

        IRepository<Category> Categories { get; }

        void Save();
    }
}

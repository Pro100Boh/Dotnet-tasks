using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL.RepositoriesAdoNet
{
    public class ADOUnitOfWork : IUnitOfWork
    {
        private SqlConnection connection;
        private IRepository<Supplier> supplierRepository;
        private IRepository<Product> productRepository;
        private IRepository<Category> categoryRepository;

        public ADOUnitOfWork(string connectionStringName)
        {
           // TODO
        }

    }
}

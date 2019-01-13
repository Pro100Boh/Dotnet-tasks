using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoriesAdoNet
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private SqlConnection connection;
		
        public SupplierRepository(SqlConnection connection)
        {
            this.connection = connection;
        }
		
		// TODO
    }
}

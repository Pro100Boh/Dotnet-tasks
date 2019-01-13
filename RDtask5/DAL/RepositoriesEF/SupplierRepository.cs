using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoriesEF
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private CatalogContext db;

        public SupplierRepository(CatalogContext context)
        {
            this.db = context;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return db.Suppliers;
        }

        public Supplier Get(int id)
        {
            return db.Suppliers.Find(id);
        }

        public void Create(Supplier supplier)
        {
            db.Suppliers.Add(supplier);
        }

        public void Update(Supplier supplier)
        {
            db.Entry(supplier).State = EntityState.Modified;
        }

        public IEnumerable<Supplier> Find(Func<Supplier, bool> predicate)
        {
            return db.Suppliers.Where(predicate).ToList();
        }

        public bool Delete(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);

            if (supplier != null)
            {
                db.Suppliers.Remove(supplier);

                return true;
            }

            return false;
        }
    }
}

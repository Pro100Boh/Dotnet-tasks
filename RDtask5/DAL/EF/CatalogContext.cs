using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class CatalogContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }

        static CatalogContext()
        {
            Database.SetInitializer<CatalogContext>(new CatalogDbInitializer());
        }

        public CatalogContext(string connectionString)
            : base(connectionString)
        {

        }

        public class CatalogDbInitializer : CreateDatabaseIfNotExists<CatalogContext>
        {
            protected override void Seed(CatalogContext db)
            {
                var category1 = new Category() { Id = 1, Name = "Category1" };
                var category2 = new Category() { Id = 2, Name = "Category2" };

                var supplier1 = new Supplier() { Id = 1, Location = "US", Name = "Supplier1" };
                var supplier2 = new Supplier() { Id = 2, Location = "UK", Name = "Supplier2" };

                var product1 = new Product() { Id = 1, Name = "Product1", Price = 1.25m, Category = category1, Supplier = supplier1 };
                var product2 = new Product() { Id = 2, Name = "Product2", Price = 3.75m, Category = category2, Supplier = supplier2 };


                db.Categories.Add(category1);
                db.Categories.Add(category2);   

                db.Suppliers.Add(supplier1);
                db.Suppliers.Add(supplier2);

                db.Products.Add(product1);
                db.Products.Add(product2);

                db.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace RDtask5
{
    public class ProductContext : DbContext
    {
        public ProductContext(): base("DbConnection")
        { }

        public DbSet<Product> Products { get; set; }
    }

    [Table("Suppliers")]
    public class Supplier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }

        [Required, MaxLength(40)]
        public string CompanyName { get; set; }

        [MaxLength(30)]
        public string ContactName { get; set; }

        [MaxLength(60)]
        public string Adress { get; set; }

        [MaxLength(15)]
        public string City { get; set; }

        [MaxLength(15)]
        public string Region { get; set; }

        [MaxLength(15)]
        public string Country { get; set; }

        [MaxLength(24)]
        public string Phone { get; set; }
    }

    [Table("Categories")]
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required, MaxLength(15)]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }

    [Table("Products")]
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required, MaxLength(40)]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }

        [ForeignKey("SupplierRefId")]
        public Supplier Supplier { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryRefId")]
        public Category Category { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }

        public int? UnitsOnOrder { get; set; }
    }
}

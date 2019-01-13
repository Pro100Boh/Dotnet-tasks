using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductsService : IDisposable
    {
        ProductDTO GetProductById(int productId);

        IEnumerable<ProductDTO> GetAllProducts();

        void AddProduct(ProductDTO product);

        void UpdateProduct(ProductDTO product);

        bool DeleteProduct(int productId);

        IEnumerable<ProductDTO> GetProductsByPrice(decimal minPrice, decimal maxPrice);

        IEnumerable<ProductDTO> GetProductsWithMinPrice();

        IEnumerable<ProductDTO> GetProductsWithMaxPrice();

        void SaveChanges();
    }
}

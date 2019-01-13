using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork uow;

        private readonly IMapper mapper;

        public ProductsService(IUnitOfWork uow)
        {
            this.uow = uow;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>().
                    ForMember(p => p.Category, opt => opt.MapFrom(c => uow.Categories.Get(c.CategoryId))).
                    ForMember(p => p.Supplier, opt => opt.MapFrom(s => uow.Suppliers.Get(s.SupplierId)));
            });

            mapper = mapperConfig.CreateMapper();
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            var products = uow.Products.GetAll();

            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }


        public ProductDTO GetProductById(int productId)
        {
            var product = uow.Products.Get(productId);

            return mapper.Map<Product, ProductDTO>(product);
        }


        public void AddProduct(ProductDTO product)
        {
            uow.Products.Create(mapper.Map<ProductDTO, Product>(product));
        }

        public void UpdateProduct(ProductDTO product)
        {
            uow.Products.Update(mapper.Map<ProductDTO, Product>(product));
        }

        public bool DeleteProduct(int productId)
        {
            return uow.Products.Delete(productId);
        }

        public IEnumerable<ProductDTO> GetProductsByCategory(int categoryId)
        {
            IEnumerable<Product> products = uow.Products.Find(p => p.CategoryId == categoryId);

            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public IEnumerable<ProductDTO> GetProductsBySupplier(int supplierId)
        {
            var products = uow.Products.Find(p => p.SupplierId == supplierId);

            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }

        public IEnumerable<ProductDTO> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            IEnumerable<Product> products = uow.Products.Find(p => p.Price >= minPrice && p.Price <= maxPrice);

            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }


        public IEnumerable<ProductDTO> GetProductsWithMaxPrice()
        {
            var products = uow.Products.GetAll();

            if (products.Any())
            {
                decimal maxPrice = products.Max(p => p.Price);

                var productsWithMaxPrice = products.Where(p => p.Price == maxPrice);

                return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(productsWithMaxPrice);
            }

            return null;
        }

        public IEnumerable<ProductDTO> GetProductsWithMinPrice()
        {
            var products = uow.Products.GetAll();

            if (products.Any())
            {
                decimal minPrice = products.Min(p => p.Price);

                var productsWithMinPrice = products.Where(p => p.Price == minPrice);

                return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(productsWithMinPrice);
            }

            return null;
        }

        public void SaveChanges()
        {
            uow.Save();
        }

        public void Dispose()
        {
            uow.Dispose();
        }

    }
}

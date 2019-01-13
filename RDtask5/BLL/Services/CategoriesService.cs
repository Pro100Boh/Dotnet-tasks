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
    public class CategoriesService : ICategoriesService
    {
        private readonly IUnitOfWork uow;

        private readonly IMapper mapper;

        public CategoriesService(IUnitOfWork uow)
        {
            this.uow = uow;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
            });

            mapper = mapperConfig.CreateMapper();
        }

        public CategoryDTO GetCategoryById(int categoryId)
        {
            var category = uow.Categories.Get(categoryId);

            return mapper.Map<Category, CategoryDTO>(category);
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = uow.Categories.GetAll();

            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);
        }

        public void AddCategory(CategoryDTO category)
        {
            uow.Categories.Create(mapper.Map<CategoryDTO, Category>(category));
        }

        public void UpdateCategory(CategoryDTO category)
        {
            uow.Categories.Update(mapper.Map<CategoryDTO, Category>(category));
        }

        public bool DeleteCategory(int categoryId)
        {
            return uow.Categories.Delete(categoryId);
        }

        public IEnumerable<SupplierDTO> GetSuppliersByCategory(int categoryId)
        {
            var productsByCategory = uow.Products.Find(p => p.CategoryId == categoryId);

            var suppliers = productsByCategory.
                            GroupBy(p => p.SupplierId).
                            Select(p => p.First().Supplier);

            return mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDTO>>(suppliers);
        }

        public IEnumerable<ProductDTO> GetProductsByCategory(int categoryId)
        {
            var products = uow.Products.Find(p => p.CategoryId == categoryId);

            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
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

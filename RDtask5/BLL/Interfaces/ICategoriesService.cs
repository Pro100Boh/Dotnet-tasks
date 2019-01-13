using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoriesService
    {
        CategoryDTO GetCategoryById(int categoryId);

        IEnumerable<CategoryDTO> GetAllCategories();

        void AddCategory(CategoryDTO product);

        void UpdateCategory(CategoryDTO category);

        bool DeleteCategory(int categoryId);

        IEnumerable<SupplierDTO> GetSuppliersByCategory(int categoryId);

        IEnumerable<ProductDTO> GetProductsByCategory(int categoryId);

        void SaveChanges();

    }
}

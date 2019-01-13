using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISuppliersService : IDisposable
    {
        SupplierDTO GetSupplierById(int supplierId);

        IEnumerable<SupplierDTO> GetAllSuppliers();

        void AddSupplier(SupplierDTO product);

        void UpdateSupplier(SupplierDTO product);

        bool DeleteSupplier(int productId);

        IEnumerable<ProductDTO> GetProductsBySupplier(int supplierId);

        IEnumerable<SupplierDTO> GetSuppliersByLocation(string location);

        void SaveChanges();
    }
}

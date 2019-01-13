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
    public class SuppliersService : ISuppliersService
    {
        private readonly IUnitOfWork uow;

        private readonly IMapper mapper;

        public SuppliersService(IUnitOfWork uow)
        {
            this.uow = uow;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Supplier, SupplierDTO>();
                cfg.CreateMap<SupplierDTO, Supplier>();
                cfg.CreateMap<Product, ProductDTO>();

            });

            mapper = mapperConfig.CreateMapper();
        }


        public IEnumerable<SupplierDTO> GetAllSuppliers()
        {
            var suppliers = uow.Suppliers.GetAll();

            return mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDTO>>(suppliers);
        }

        public SupplierDTO GetSupplierById(int supplierId)
        {
            var supplier = uow.Suppliers.Get(supplierId);

            return mapper.Map<Supplier, SupplierDTO>(supplier);
        }

        public void AddSupplier(SupplierDTO supplier)
        {
            uow.Suppliers.Create(mapper.Map<SupplierDTO, Supplier>(supplier));
        }


        public void UpdateSupplier(SupplierDTO supplier)
        {
            uow.Suppliers.Update(mapper.Map<SupplierDTO, Supplier>(supplier));
        }

        public bool DeleteSupplier(int supplierId)
        {
            return uow.Suppliers.Delete(supplierId);
        }

        public IEnumerable<ProductDTO> GetProductsBySupplier(int supplierId)
        {
            var products = uow.Products.Find(p => p.SupplierId == supplierId);

            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }



        public IEnumerable<SupplierDTO> GetSuppliersByLocation(string location)
        {
            var suppliers = uow.Suppliers.Find(s => s.Location == location);

            return mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierDTO>>(suppliers);
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

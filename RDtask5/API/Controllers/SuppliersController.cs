using API.Models;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class SuppliersController : ApiController
    {
        private ISuppliersService suppliersService;

        private IMapper mapper;

        public SuppliersController(ISuppliersService service)
        {
            suppliersService = service;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDTO, ProductView>();
                cfg.CreateMap<SupplierDTO, SupplierView>();
                cfg.CreateMap<SupplierView, SupplierDTO>();
            });

            mapper = config.CreateMapper();
        }

        [Route("api/suppliers/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var supplier = suppliersService.GetSupplierById(id);

            if (supplier == null)
            {
                return NotFound();
            }

            var supplierView = mapper.Map<SupplierDTO, SupplierView>(supplier);

            return Ok(supplierView);
        }

        [Route("api/suppliers")]
        [HttpGet]
        public IEnumerable<SupplierView> GetAll()
        {
            var suppliers = suppliersService.GetAllSuppliers();

            return mapper.Map<IEnumerable<SupplierDTO>, IEnumerable<SupplierView>>(suppliers);
        }

        [Route("api/suppliers")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]SupplierView supplier)
        {
            var supplierDTO = mapper.Map<SupplierView, SupplierDTO>(supplier);

            suppliersService.AddSupplier(supplierDTO);

            suppliersService.SaveChanges();

            return Ok();
        }

        [Route("api/suppliers")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]SupplierView product)
        {
            var supplierDTO = mapper.Map<SupplierView, SupplierDTO>(product);

            suppliersService.UpdateSupplier(supplierDTO);

            suppliersService.SaveChanges();

            return Ok();
        }

        [Route("api/suppliers/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (suppliersService.DeleteSupplier(id))
            {
                suppliersService.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/suppliers/{id:int}/products")]
        [HttpGet]
        public IEnumerable<ProductView> GetProductsBySupplier(int id)
        {
            var products = suppliersService.GetProductsBySupplier(id);

            return mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductView>>(products);
        }

        [Route("api/suppliers/location/{location}")]
        [HttpGet]
        public IEnumerable<SupplierView> GetSuppliersByLocation(string location)
        {
            var suppliers = suppliersService.GetSuppliersByLocation(location);

            return mapper.Map<IEnumerable<SupplierDTO>, IEnumerable<SupplierView>>(suppliers);
        }



    }
}

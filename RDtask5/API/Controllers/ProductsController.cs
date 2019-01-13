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
    public class ProductsController : ApiController
    {
        private IProductsService productsService;

        private IMapper mapper;

        public ProductsController(IProductsService service)
        {
            productsService = service;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDTO, ProductView>();
                cfg.CreateMap<ProductView, ProductDTO>();
            });

            mapper = config.CreateMapper();
        }

        [Route("api/products/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var product = productsService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            var productView = mapper.Map<ProductDTO, ProductView>(product);

            return Ok(productView);
        }

        [Route("api/products")]
        [HttpGet]
        public IEnumerable<ProductView> GetAll()
        {
            var products = productsService.GetAllProducts();

            return mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductView>>(products);
        }

        [Route("api/products")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]ProductView product)
        {
            var productDTO = mapper.Map<ProductView, ProductDTO>(product);

            productsService.AddProduct(productDTO);

            productsService.SaveChanges();

            return Ok();
        }

        [Route("api/products")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]ProductView product)
        {
            var productDTO = mapper.Map<ProductView, ProductDTO>(product);

            productsService.UpdateProduct(productDTO);

            productsService.SaveChanges();

            return Ok();
        }

        [Route("api/products/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (productsService.DeleteProduct(id))
            {
                productsService.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/products/minprice")]
        [HttpGet]
        public IEnumerable<ProductView> MinPrice()
        {
            var products = productsService.GetProductsWithMinPrice();

            return mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductView>>(products);
        }

        [Route("api/products/maxprice")]
        [HttpGet]
        public IEnumerable<ProductView> MaxPrice()
        {
            var products = productsService.GetProductsWithMaxPrice();

            return mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductView>>(products);
        }

        [Route("api/products/price/{min}/{max}")]
        [HttpGet]
        public IEnumerable<ProductView> GetProductsByPrice(decimal min, decimal max)
        {
            var products = productsService.GetProductsByPrice(min, max);

            return mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductView>>(products);
        }
    }
}

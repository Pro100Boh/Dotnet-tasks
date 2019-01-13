using API.Models;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class CategoriesController : ApiController
    {
        private ICategoriesService categoriesService;

        private IMapper mapper;

        public CategoriesController(ICategoriesService service)
        {
            categoriesService = service;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryDTO, CategoryView>();
                cfg.CreateMap<CategoryView, CategoryDTO>();
                cfg.CreateMap<ProductDTO, ProductView>();
                cfg.CreateMap<SupplierDTO, SupplierView>();
            });

            mapper = config.CreateMapper();
        }


        [Route("api/categories/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var category = categoriesService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }
            var categoryView = mapper.Map<CategoryDTO, CategoryView>(category);

            return Ok(categoryView);
        }

        [Route("api/categories")]
        [HttpGet]
        public IEnumerable<CategoryView> GetAll()
        {
            var products = categoriesService.GetAllCategories();

            return mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryView>>(products);
        }

        [Route("api/categories")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]CategoryView category)
        {
            var categoryDTO = mapper.Map<CategoryView, CategoryDTO>(category);

            categoriesService.AddCategory(categoryDTO);

            categoriesService.SaveChanges();

            return Ok();
        }

        [Route("api/categories")]
        [HttpPut]
        public IHttpActionResult Update([FromBody]CategoryView category)
        {
            var categoryDTO = mapper.Map<CategoryView, CategoryDTO>(category);

            categoriesService.UpdateCategory(categoryDTO);

            categoriesService.SaveChanges();

            return Ok();
        }

        [Route("api/categories/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (categoriesService.DeleteCategory(id))
            {
                categoriesService.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("api/categories/{id:int}/products")]
        [HttpGet]
        public IEnumerable<ProductView> GetProductsByCategory(int id)
        {
            var products = categoriesService.GetProductsByCategory(id);

            return mapper.Map<IEnumerable<ProductDTO>, IEnumerable<ProductView>>(products);
        }

        [Route("api/categories/{id:int}/suppliers")]
        [HttpGet]
        public IEnumerable<SupplierView> GetSuppliersByCategory(int id)
        {
            var suppliers = categoriesService.GetSuppliersByCategory(id);

            return mapper.Map<IEnumerable<SupplierDTO>, IEnumerable<SupplierView>>(suppliers);
        }
    }
}
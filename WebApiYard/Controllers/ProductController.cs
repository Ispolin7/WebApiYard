using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiYard.Common;
using WebApiYard.Services;
using WebApiYard.Services.Interfaces;

namespace WebApiYard.Controllers
{
    [Route("api/products")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        /// <summary>
        /// ProductController constructor.
        /// </summary>
        /// <param name="mapper"></param>
        public ProductController(IMapper mapper)
        {
            this.productService = new ProductService();
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        /// <returns>Product collection</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Services.Models.Product>> GetAll()
        {
            return this.productService.All().ToList();
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product information</returns>
        [HttpGet("{id:Guid}")]
        public ActionResult<Services.Models.Product> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.NotFound();
            }
            // TODO 500 delete == true
            return this.productService.Get(id);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>new Product's id</returns>
        [HttpPost]
        public ActionResult Post([FromBody] ValidationModels.ProductCreate product)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var productServiceModel = this.mapper.Map<Services.Models.Product>(product);
            var id = this.productService.Save(productServiceModel);
            return this.Created("", new { Id = id });
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>updated Product</returns>
        [HttpPut("{id:Guid}")]
        public ActionResult Put(Guid id, [FromBody]  ValidationModels.ProductUpdate product)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ValidationProblem();
            }

            var productServiceModel = this.mapper.Map<Services.Models.Product>(product);

            if (this.productService.Update(productServiceModel))
            {
                return this.Ok(new { success = true });
            }
            return this.BadRequest();

        }

        /// <summary>
        /// Remove the specified resource from storage.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>no contetnt</returns>
        [HttpDelete("{id:Guid}")]
        public ActionResult Delete(Guid id)
        {
            if (!this.productService.Remove(id))
            {
                return this.BadRequest();
            }
            return this.NoContent();
        }

        //[HttpGet("{test}")]
        //public ActionResult<object> test(Guid id)
        //{
        //    var str = "Yellow";
        //    var res = Enum.GetNames(typeof(Colors));
        //    //Array.Exists(res, element => element == str);
        //    return new { num = Array.Exists(res, element => element == str) };

        //}
    }
}
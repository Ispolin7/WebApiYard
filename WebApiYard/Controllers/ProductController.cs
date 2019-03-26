using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiYard.Controllers.ValidationModels;
using WebApiYard.Services;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

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
        public async Task<ActionResult<IEnumerable<ProductServiceModel>>> GetAllAsync()
        {
            return Ok(await this.productService.AllAsync());
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product information</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ProductServiceModel>> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.NotFound();
            }
            return await this.productService.GetAsync(id);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>new Product's id</returns>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] ProductCreate product)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var productServiceModel = this.mapper.Map<ProductServiceModel>(product);
            var id = await this.productService.SaveAsync(productServiceModel);
            return this.Created("", new { Id = id });
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>updated Product</returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] ProductUpdate product)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ValidationProblem();
            }

            var productServiceModel = this.mapper.Map<ProductServiceModel>(product);

            if (await this.productService.UpdateAsync(productServiceModel))
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
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!await this.productService.RemoveAsync(id))
            {
                return this.BadRequest();
            }
            return this.NoContent();
        }
    }
}
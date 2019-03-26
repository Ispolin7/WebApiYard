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
    [Route("api/orders")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        /// <summary>
        /// OrderController constructor
        /// </summary>
        /// <param name="mapper"></param>
        public OrderController(IMapper mapper)
        {
            this.orderService = new OrderService();
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        /// <returns>Order collection</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderServiceModel>>> GetAllAsync()
        {
            return Ok(await this.orderService.AllAsync());
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order information</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<OrderServiceModel>> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.NotFound();
            }

            return await this.orderService.GetAsync(id);           
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>new Order's id</returns>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] OrderCreate order)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var orderServiceModel = this.mapper.Map<OrderServiceModel>(order);
            var id = await this.orderService.SaveAsync(orderServiceModel);
            return this.Created("", new { Id = id });
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns>updated Order</returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody]  OrderUpdate order)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ValidationProblem();
            }
            
            var orderServiceModel = this.mapper.Map<OrderServiceModel>(order);

            if (await this.orderService.UpdateAsync(orderServiceModel))
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
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            if (!await this.orderService.RemoveAsync(id))
            {
                return this.BadRequest();
            }
            return this.NoContent();
        }
    }
}
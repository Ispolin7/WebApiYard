using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiYard.Controllers.ValidationModels;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYard.Controllers
{
    [Route("api/order_items")]
    //[Produces("application/json")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService orderItemService;
        private readonly IMapper mapper;

        /// <summary>
        /// OrderItemController constructor
        /// </summary>
        /// <param name="mapper"></param>
        public OrderItemController(IMapper mapper, IOrderItemService service)
        {
            this.orderItemService = service ?? throw new ArgumentNullException(nameof(service));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        /// <returns>OrderItems collection</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemServiceModel>>> GetAllAsync()
        {
            return Ok(await this.orderItemService.AllAsync());
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderItem information</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<OrderItemServiceModel>> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.NotFound();
            }

            return await this.orderItemService.GetAsync(id);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns>new OrderItem's id</returns>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] OrderItemCreate orderItem)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    return this.BadRequest();
            //}

            var orderItemServiceModel = this.mapper.Map<OrderItemServiceModel>(orderItem);
            var id = await this.orderItemService.SaveAsync(orderItemServiceModel);
            return this.Created("", new { Id = id });
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderItem"></param>
        /// <returns>updated OrderItem</returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] OrderItemUpdate orderItem)
        {
            //if (!this.ModelState.IsValid)
            //{
            //    return this.ValidationProblem();
            //}

            var orderItemServiceModel = this.mapper.Map<OrderItemServiceModel>(orderItem);

            if (await this.orderItemService.UpdateAsync(orderItemServiceModel))
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
            if (!await this.orderItemService.RemoveAsync(id))
            {
                return this.BadRequest();
            }
            return this.NoContent();
        }
    }
}
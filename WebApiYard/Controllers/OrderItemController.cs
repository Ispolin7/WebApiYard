using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiYard.Controllers.ViewModels;
using WebApiYard.Services;
using WebApiYard.Services.Interfaces;

namespace WebApiYard.Controllers
{
    [Route("api/order_items")]
    [Produces("application/json")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService orderItemService;
        private readonly IMapper mapper;

        /// <summary>
        /// OrderItemController constructor
        /// </summary>
        /// <param name="mapper"></param>
        public OrderItemController(IMapper mapper)
        {
            this.orderItemService = new OrderItemService();
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        /// <returns>OrderItems collection</returns>
        [HttpGet]
        public ActionResult<IEnumerable<OrderItem>> GetAll()
        {
            return this.orderItemService.All().ToList();
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderItem information</returns>
        [HttpGet("{id:Guid}")]
        public ActionResult<OrderItem> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.NotFound();
            }

            return this.orderItemService.Get(id);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns>new OrderItem's id</returns>
        [HttpPost]
        public ActionResult Post([FromBody] ValidationModels.OrderItemCreate orderItem)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var orderItemServiceModel = this.mapper.Map<OrderItem>(orderItem);
            var id = this.orderItemService.Save(orderItemServiceModel);
            return this.Created("", new { Id = id });
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderItem"></param>
        /// <returns>updated OrderItem</returns>
        [HttpPut("{id:Guid}")]
        public ActionResult Put(Guid id, [FromBody]  ValidationModels.OrderItemUpdate orderItem)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ValidationProblem();
            }

            var orderItemServiceModel = this.mapper.Map<OrderItem>(orderItem);

            if (this.orderItemService.Update(orderItemServiceModel))
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
            if (!this.orderItemService.Remove(id))
            {
                return this.BadRequest();
            }
            return this.NoContent();
        }
    }
}
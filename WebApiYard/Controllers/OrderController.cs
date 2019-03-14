using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiYard.Services;
using WebApiYard.Services.Interfaces;

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
        public ActionResult<IEnumerable<Services.Models.Order>> GetAll()
        {
            return this.orderService.All().ToList();
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order information</returns>
        [HttpGet("{id:Guid}")]
        public ActionResult<Services.Models.Order> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.NotFound();
            }

            return this.orderService.Get(id);           
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>new Order's id</returns>
        [HttpPost]
        public ActionResult Post([FromBody] ValidationModels.OrderCreate order)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var orderServiceModel = this.mapper.Map<Services.Models.Order>(order);
            var id = this.orderService.Save(orderServiceModel);
            return this.Created("", new { Id = id });
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="order"></param>
        /// <returns>updated Order</returns>
        [HttpPut("{id:Guid}")]
        public ActionResult Put(Guid id, [FromBody]  ValidationModels.OrderUpdate order)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ValidationProblem();
            }
            
            var orderServiceModel = this.mapper.Map<Services.Models.Order>(order);

            if (this.orderService.Update(orderServiceModel))
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
            if (!this.orderService.Remove(id))
            {
                return this.BadRequest();
            }
            return this.NoContent();
        }
    }
}
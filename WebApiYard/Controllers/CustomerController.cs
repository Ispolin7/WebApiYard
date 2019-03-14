using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiYard.Services;
using WebApiYard.Services.Interfaces;

namespace WebApiYard.Controllers
{
    [Route("api/customers")]
    [Produces("application/json")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        /// <summary>
        /// CustomerController constructor
        /// </summary>
        /// <param name="mapper"></param>
        public CustomerController(IMapper mapper)
        {
            this.customerService = new CustomerService();
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        ///// <summary>
        ///// Constructor for tests
        ///// </summary>
        ///// <param name="mapper"></param>
        ///// <param name="service"></param>
        //public CustomerController(/*IMapper mapper, */ICustomerService service)
        //{
        //    this.customerService = service;
        //    //this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        //}


        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        /// <returns>Customers collection</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ViewModels.Customer>> GetAll()
        {
            var customers = this.customerService.All();
            var mapped = this.mapper.Map<IEnumerable<ViewModels.Customer>>(customers);
            return mapped.ToList();
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer information</returns>
        [HttpGet("{id:Guid}")]
        public ActionResult<Controllers.ViewModels.Customer> GetById(Guid id)
        {
            if(id == Guid.Empty)
            {
                return this.NotFound();
            }

            var customer = this.customerService.Get(id);
            return this.mapper.Map<ViewModels.Customer>(customer);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>new customer id</returns>
        [HttpPost]
        public ActionResult Post([FromBody] Controllers.ViewModels.Customer customer)
        {
            if(!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var customerServiceModel = this.mapper.Map<Services.Models.Customer>(customer);
            var id = this.customerService.Save(customerServiceModel);
            return this.Created("", new { Id = id});
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns>updated customer</returns>
        [HttpPut("{id:Guid}")]
        public ActionResult Put(Guid id, [FromBody]  Controllers.ViewModels.Customer customer)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ValidationProblem();
            }
            customer.Id = id;
            var customerServiceModel = this.mapper.Map<Services.Models.Customer>(customer);

            if (customerService.Update(customerServiceModel))
            {
                return this.Ok(new { success = true});
            }
            return this.BadRequest();

        }

        /// <summary>
        /// Remove the specified resource from storage.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public ActionResult Delete(Guid id)
        {
            if (!customerService.Remove(id))
            {
                return this.BadRequest();
            }
            return this.NoContent();
        }

       // TODO test after realization method in service
        [HttpGet("{id:Guid}/include")]
        public ActionResult<Repositories.Models.Customer> Include(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.NotFound();
            }

           // var customer = this.customerService.GetInclude(id);
            return this.customerService.GetInclude(id);
        }
    }
}
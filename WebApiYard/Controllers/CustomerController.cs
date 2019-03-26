using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiYard.Controllers.ViewModels;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

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
        public CustomerController(IMapper mapper, ICustomerService service)
        {
            this.customerService = service ?? throw new ArgumentNullException(nameof(service));
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
        public async Task<ActionResult<IEnumerable<CustomerView>>> GetAllAsync()
        {
            var customers = await this.customerService.AllAsync();
            var mapped = this.mapper.Map<IEnumerable<CustomerView>>(customers);
            return mapped.ToList();
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer information</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomerView>> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var customer = await this.customerService.GetAsync(id);
            return this.mapper.Map<CustomerView>(customer);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>new customer id</returns>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] CustomerView customer)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
                // TODO test return this.ValidationProblem();
            }

            var customerServiceModel = this.mapper.Map<CustomerServiceModel>(customer);
            var id = await this.customerService.SaveAsync(customerServiceModel);
            return this.Created("", new { Id = id });
        }

        /// <summary>
        /// Update the specified resource in storage.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns>updated customer</returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] CustomerView customer)
        {
            if (!this.ModelState.IsValid)
            {
                return this.ValidationProblem();
            }

            //customer.Id = id;
            var customerServiceModel = this.mapper.Map<CustomerServiceModel>(customer);

            if (await customerService.UpdateAsync(customerServiceModel))
            {
                return this.Ok(new { success = true });
            }
            return this.BadRequest();

        }

        /// <summary>
        /// Remove the specified resource from storage.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            if (!await customerService.RemoveAsync(id))
            {
                return this.BadRequest();
            }
            return this.NoContent();
        }
    }
}
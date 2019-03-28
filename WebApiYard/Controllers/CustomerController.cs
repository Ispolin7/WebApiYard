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

        /// <summary>
        /// Display a listing of the resource.
        /// </summary>
        /// <returns>Customers collection</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerServiceModel>>> GetAllAsync()
        {
            //var customers = await this.customerService.AllAsync();
            //var mapped = this.mapper.Map<IEnumerable<CustomerView>>(customers);
            //return mapped.ToList();
            return Ok(await this.customerService.AllAsync());
        }

        /// <summary>
        /// Display the specified resource.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer information</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomerServiceModel>> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            return await this.customerService.GetAsync(id);
        }

        /// <summary>
        /// Store a newly created resource in storage.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>new customer id</returns>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] CustomerValidation customer)
        {
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
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] CustomerValidation customer)
        {
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
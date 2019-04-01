using System;
using System.Collections.Generic;
using WebApiYard.Repositories;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;
using WebApiYard.Mappings;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiYard.Repositories.Models;

namespace WebApiYard.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly RepositoryToServiceMapper upMapper;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public CustomerService(IRepository<Customer> repository)
        {
            this.customerRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.upMapper = new RepositoryToServiceMapper();
        }

        /// <summary>
        /// Get all customers from DB
        /// </summary>
        /// <returns>Customer collection</returns>
        public async Task<IEnumerable<CustomerServiceModel>> AllAsync()
        {
            var customers = await this.customerRepository
                .All()
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Items)
                        .ThenInclude(i => i.Product)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.ShippingAddress)
                .ToListAsync();
            return this.upMapper.MapCustomers(customers);
        }

        /// <summary>
        /// Get Customer information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer or throw an exception</returns>
        public async Task<CustomerServiceModel> GetAsync(Guid id)
        {
            var customer = await customerRepository
                .GetById(id)
                //.Including(c => c.Orders)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Items)
                        .ThenInclude(i => i.Product)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.ShippingAddress)
                .FirstOrDefaultAsync();
            return this.upMapper.MapCustomer(customer);
        }

        /// <summary>
        /// Add new Customer to DB
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>new Customer's id</returns>
        public async Task<Guid> SaveAsync(CustomerServiceModel customer)
        {
            var repositoryCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age               
            };
            return await customerRepository.InsertAsync(repositoryCustomer);
        }

        /// <summary>
        /// Update Customer information in DB
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>result status</returns>
        public async Task<bool> UpdateAsync(CustomerServiceModel customer)
        {
            var oldCustomer = await this.GetCustomerFromDBAsync(customer.Id);
            var updatedCusromer = customer.UpdateProperties(oldCustomer);
            // TODO 500
            return await customerRepository.UpdateAsync(updatedCusromer);
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result status</returns>
        public async Task<bool> RemoveAsync(Guid id)
        {
            var customer = await this.GetCustomerFromDBAsync(id);
            customer.IsDelete = true;
            customer.UpdatedAt = DateTime.Now;
            return await customerRepository.UpdateAsync(customer);
        }

        /// <summary>
        /// Get Customer from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer model or throw an exception</returns>
        public async Task<Customer> GetCustomerFromDBAsync(Guid id)
        {
            var customer = await customerRepository.GetById(id).FirstAsync();
            if (customer == null || customer.IsDelete == true)
            {
                throw new ArgumentException("Customer not found");
            }
            return customer;
        }       
    }
}

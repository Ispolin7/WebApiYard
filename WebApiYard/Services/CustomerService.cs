using System;
using System.Linq;
using System.Collections.Generic;
using WebApiYard.Repositories;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYard.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Repositories.Models.Customer> customerRepository;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public CustomerService()
        {
            customerRepository = new Repository<Repositories.Models.Customer>();
        }

        /// <summary>
        /// Get all customers from DB
        /// </summary>
        /// <returns>Customer collection</returns>
        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = this.customerRepository.All();

            return customers.Where(c => c.IsDelete == false).Select(x => new Customer
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                // TODO implement logic for orders
                Orders = new List<string>()
            });
        }

        /// <summary>
        /// Get Customer information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer or throw an exception</returns>
        public Customer GetCustomer(Guid id)
        {

            var customer = this.GetCustomerFromDB(id);
            return new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                // TODO implement logic for orders
                Orders = new List<string>()
            };
        }

        /// <summary>
        /// Add new Customer to DB
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>new Customer's id</returns>
        public Guid SaveCustomer(Customer customer)
        {
            var repositoryCustomer = new Repositories.Models.Customer
            {
                Id = Guid.NewGuid(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                CreatedAT = DateTime.UtcNow,
                UpdatedAt = DateTime.MinValue,
                IsDelete = false
            };
            return customerRepository.Insert(repositoryCustomer);
        }

        /// <summary>
        /// Update Customer information in DB
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>result status</returns>
        public bool UpdateCustomer(Customer customer)
        {
            var oldCustomer = this.GetCustomerFromDB(customer.Id);
            var newCustomer = new Repositories.Models.Customer
            {
                Id = oldCustomer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                CreatedAT = oldCustomer.CreatedAT,
                UpdatedAt = DateTime.UtcNow,
                IsDelete = false
            };
            return customerRepository.Update(newCustomer);
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result status</returns>
        public bool RemoveCustomer(Guid id)
        {
            var customer = this.GetCustomerFromDB(id);
            customer.IsDelete = true;
            return customerRepository.Update(customer);
        }

        /// <summary>
        /// Get Customer from repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer model or throw an exception</returns>
        public Repositories.Models.Customer GetCustomerFromDB(Guid id)
        {
            var customer = customerRepository.GetById(id);
            if (customer == null || customer.IsDelete == true)
            {
                throw new ArgumentException("exception");
            }
            return customer;
        }
    }
}

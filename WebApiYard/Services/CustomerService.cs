using System;
using System.Linq;
using System.Collections.Generic;
using WebApiYard.Repositories;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiYard.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Repositories.Models.Customer> customerRepository;
        //private readonly IRepository<Repositories.Models.Order> orderRepository;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public CustomerService()
        {
            this.customerRepository = new Repository<Repositories.Models.Customer>();
            //this.orderRepository = new Repository<Repositories.Models.Order>();
        }

        //public CustomerService(Repository<Repositories.Models.Customer> repository)
        //{
        //    this.customerRepository = repository;
        //}

        /// <summary>
        /// Get all customers from DB
        /// </summary>
        /// <returns>Customer collection</returns>
        public IEnumerable<Customer> All()
        {
            var customers = this.customerRepository.All();

            return customers.Where(c => c.IsDelete == false).Select(x => new Customer
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                // TODO implement logic for orders
                //Orders = new List<string>()
            });
        }

        /// <summary>
        /// Get Customer information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer or throw an exception</returns>
        public Customer Get(Guid id)
        {

            var customer = this.GetCustomerFromDB(id);
            return new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                // TODO implement logic for orders
                //Orders = new List<string>()
            };
        }

        /// <summary>
        /// Add new Customer to DB
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>new Customer's id</returns>
        public Guid Save(Customer customer)
        {
            var repositoryCustomer = new Repositories.Models.Customer
            {
                //Id = Guid.NewGuid(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                CreatedAT = DateTime.UtcNow,
                UpdatedAt = DateTime.MinValue,
                //IsDelete = false
            };
            return customerRepository.Insert(repositoryCustomer);
        }

        /// <summary>
        /// Update Customer information in DB
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>result status</returns>
        public bool Update(Customer customer)
        {
            var oldCustomer = this.GetCustomerFromDB(customer.Id);
            var newCustomer = new Repositories.Models.Customer
            {
                Id = oldCustomer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                //CreatedAT = oldCustomer.CreatedAT,
                UpdatedAt = DateTime.UtcNow,
                //IsDelete = false
            };
            return customerRepository.Update(newCustomer);
        }

        /// <summary>
        /// SoftDelete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result status</returns>
        public bool Remove(Guid id)
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
                throw new ArgumentException("Entity not found");
            }
            return customer;
        }

        // TODO победить это)))
        public Repositories.Models.Customer GetInclude(Guid id)
        {
            var customer = customerRepository
                .AllIncluding(
                    //c => c.Id == id,
                    // o => o.IsDelete != true,
                    //"Orders"
                    c => c.Orders.Select(o => o.ShippingAddress)
                    //c => c.Orders

                    );
            return customer.First();
            //return new Repositories.Models.Customer();
            //var customer = this.customerRepository.Get(param => param
            //.Include(c => c.Orders)
            //    .ThenInclude(o => o.ShippingAddress)
            //.Include(c => c.Orders)
            //    .ThenInclude(o => o.Items)
            //.ThenInclude(i => i.Product))
            //.First();
            //return customer;
        }
    }
}

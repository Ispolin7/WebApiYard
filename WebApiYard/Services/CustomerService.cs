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
            this.customerRepository = new Repository<Repositories.Models.Customer>();
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
            });
        }

        /// <summary>
        /// Get Customer information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer or throw an exception</returns>
        public Customer Get(Guid id)
        {
            // TODO Exception
            var customer = customerRepository.AllIncluding(c => c.Orders).FirstOrDefault();
            return new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                Orders = customer.Orders        
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
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age               
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
            oldCustomer.FirstName = customer.FirstName;
            oldCustomer.LastName = customer.LastName;
            oldCustomer.Age = customer.Age;
            oldCustomer.UpdatedAt = DateTime.Now;
            return customerRepository.Update(oldCustomer);
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
            customer.UpdatedAt = DateTime.Now;
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
                throw new ArgumentException("Customer not found");
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
                    //c => c.Orders.Select(o => o.ShippingAddress)
                    //c => c.Orders

                    );
            return customer.First();
        }
    }
}

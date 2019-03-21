using System;
using System.Linq;
using System.Collections.Generic;
using WebApiYard.Repositories;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;
using WebApiYard.Mappings;

namespace WebApiYard.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Repositories.Models.Customer> customerRepository;
        private readonly RepositoryToServiceMapper upMapper;

        /// <summary>
        /// Initialization of all repositories
        /// </summary>
        public CustomerService()
        {
            this.customerRepository = new Repository<Repositories.Models.Customer>();
            this.upMapper = new RepositoryToServiceMapper();
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
            var customers = this.customerRepository.All().Including(c => c.Orders);
            return this.upMapper.MapCustomers(customers);
        }

        /// <summary>
        /// Get Customer information from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Customer or throw an exception</returns>
        public Customer Get(Guid id)
        {
            var customer = customerRepository.GetById(id).Including(c => c.Orders).FirstOrDefault();
            return this.upMapper.MapCustomer(customer);
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
            var customer = customerRepository.GetById(id).FirstOrDefault();
            if (customer == null || customer.IsDelete == true)
            {
                throw new ArgumentException("Customer not found");
            }
            return customer;
        }       
    }
}

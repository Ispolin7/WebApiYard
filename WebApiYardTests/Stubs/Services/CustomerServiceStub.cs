using System;
using System.Collections.Generic;
using System.Text;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYardTests.Stubs.Services
{
    class CustomerServiceStub : ICustomerService
    {
        public IEnumerable<Customer> GetAllCustomers()
        {
            return new List<Customer> { default(Customer), default(Customer) };
        }

        public Customer GetCustomer(Guid id)
        {
            return new WebApiYard.Services.Models.Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Name",
                Age = 27,
                Orders = new List<string> { "first order", "second order"}
            };
        }

        public bool RemoveCustomer(Guid id)
        {
            return true;
        }

        public Guid SaveCustomer(Customer customer)
        {
            return Guid.NewGuid();
        }

        public bool UpdateCustomer(Customer customer)
        {
            return true;
        }
    }
}

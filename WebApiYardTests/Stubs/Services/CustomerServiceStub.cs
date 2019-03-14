using System;
using System.Collections.Generic;
using System.Text;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYardTests.Stubs.Services
{
    class CustomerServiceStub : ICustomerService
    {
        public IEnumerable<Customer> All()
        {
            return new List<Customer> { default(Customer), default(Customer) };
        }

        public Customer Get(Guid id)
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

        public WebApiYard.Repositories.Models.Customer GetInclude(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid id)
        {
            return true;
        }

        public Guid Save(Customer customer)
        {
            return Guid.NewGuid();
        }

        public bool Update(Customer customer)
        {
            return true;
        }
    }
}

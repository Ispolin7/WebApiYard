using System;
using System.Collections.Generic;
using System.Text;
using WebApiYard.Repositories.Models;

namespace WebApiYardTests
{
    public static class TestCustomersCollection
    {
        public static Customer TestCustomer;
        public static IEnumerable<Customer> Collection;

        static TestCustomersCollection()
        {
            TestCustomer = new Customer
            {
                Id = Guid.Parse("edb050f5-ca3b-4381-a4ba-7b57ebeb108f"),
                Age = 21,
                FirstName = "First",
                LastName = "Customer"
            };
            Collection = new List<Customer>
            {
                TestCustomer,
                new Customer { Age = 21, FirstName = "Second", LastName = "Customer" },
                new Customer { Age = 21, FirstName = "Third", LastName = "Customer" }
            };
        }       
    }
}

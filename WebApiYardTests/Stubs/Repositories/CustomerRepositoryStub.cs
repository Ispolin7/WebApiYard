using System;
using System.Collections.Generic;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;

namespace UnitTests.Stubs
{
    public class CustomerRepositoryStub
    {
        public Repository<Customer> Customers { get; set; }
        public Customer TestCustomer { get; set; }

        public CustomerRepositoryStub()
        {
            this.Customers = new Repository<Customer>();
            Repository<Customer>._entities = new Dictionary<Guid, Customer>();
            this.TestCustomer = new Customer
            {
                Id = Guid.Parse("edb050f5-ca3b-4381-a4ba-7b57ebeb108f"),
                FirstName = "Poor",
                LastName = "Gaper",
                Age = 20
            };

            Repository<Customer>._entities.Add(TestCustomer.Id, TestCustomer);
            Repository<Customer>._entities.Add(
                Guid.Parse("02aaa926-2d33-4935-9e55-478d4b10d988"),
                new Customer
                {
                    Id = Guid.Parse("02aaa926-2d33-4935-9e55-478d4b10d988"),
                    FirstName = "Rich",
                    LastName = "Buyer"                  
                }
            );
            Repository<Customer>._entities.Add(
                Guid.Parse("02aaa926-2d33-4935-9e55-478d4b10d777"),
                new Customer
                {
                    Id = Guid.Parse("02aaa926-2d33-4935-9e55-478d4b10d777"),
                    FirstName = "Stray",
                    LastName = "Guy"
                }
            );
        }
    }
}

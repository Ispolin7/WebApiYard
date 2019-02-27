using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using UnitTests.Stubs;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;
using WebApiYard.Services;

namespace UnitTests.Services
{
    [TestClass]
    public class CustomerServiceTests
    {
        public CustomerService Service { get; set; }

        public Repository<Customer> TestRepositoryCustomer { get; set; }       

        public Customer TestCustomer { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var customerStub = new CustomerRepositoryStub();            
            this.TestRepositoryCustomer = customerStub.Customers;           
            this.TestCustomer = customerStub.TestCustomer;
            Service = new CustomerService(TestRepositoryCustomer);
        }

        [TestMethod]
        public void GetAllCustomers_ExpectedCount3()
        {
            var customers = Service.GetAllCustomers();

            var count = customers.Count();

            Assert.IsTrue(count == 3, $"Emount of elements - {count},expected - 3");
        }

        [TestMethod]
        public void AddNewCustomer_Expected4Customers()
        {
            var newServiceCustomer = new WebApiYard.Services.Models.Customer
            {
                FirstName = "Test",
                LastName = "Name",
                Age = 20
            };

            Service.SaveCustomer(newServiceCustomer);
            var customersCount = Repository<Customer>._entities.Count;

            Assert.IsTrue(customersCount == 4, $"Emount of elements - {customersCount}, expected - 4");
        }

        [TestMethod]
        public void GetCustomer_ExpectedNameGaper()
        {
            var customer = Service.GetCustomer(TestCustomer.Id);

            var customerName = customer.LastName;

            Assert.IsTrue(customerName == TestCustomer.LastName, $"Current Name - {customerName}, but expected - {TestCustomer.LastName}");
        }

        [TestMethod]
        public void Update_ExpectedSuccess()
        {
            var serviceCustomer = new WebApiYard.Services.Models.Customer
            {
                Id = TestCustomer.Id,
                FirstName = "Test",
                LastName = "Name",
                Age = TestCustomer.Age
            };
            
            var result = Service.UpdateCustomer(serviceCustomer);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Remove_Expected2Customers()
        {
            Service.RemoveCustomer(TestCustomer.Id);

            var customerCount = Service.GetAllCustomers().Count();

            Assert.IsTrue(customerCount == 2, $"Emount of elements - {customerCount}, expected - 2");
        }

        [TestMethod]
        public void GetCustomerFromDB_ExpectedNotNullValue()
        {
            var customer = Service.GetCustomerFromDB(TestCustomer.Id);

            var result = customer != null ? true : false;

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Customer not found in repository")]
        public void GetCustomerFromDB_ExpectedException()
        {
            Service.GetCustomerFromDB(Guid.NewGuid());
        }
    }
}

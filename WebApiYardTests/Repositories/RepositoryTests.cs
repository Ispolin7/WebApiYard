using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.DAL;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;

namespace WebApiYardTests.Repositories
{
    [TestClass]
    public class RepositoryTests
    {
        private ApiContext context;
        private Repository<Customer> repository;
        private int defaultEntityCount;
        private Customer TestCustomer;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = new TestDbContextFactory();
            this.context = factory.GetContext();
            factory.FillDb(TestCustomersCollection.Collection);

            this.TestCustomer = TestCustomersCollection.TestCustomer;
            this.defaultEntityCount = TestCustomersCollection.Collection.Count();
            this.repository = new Repository<Customer>(context); 
        }

        // TODO Cleanup method not work
        //[TestCleanup]
        //public void TestCleanup()
        //{;
        //    //context.Dispose();
        //}




        [TestMethod]
        public void All_GetAllCustomers_Expected3EntityInDb()
        {
            // Arrange
            var customers = this.repository.All().ToList();

            // Act
            var count = customers.Count;
            
            // Assert
            Assert.IsTrue(this.defaultEntityCount == count, $"Expected {defaultEntityCount} entities");
        }

        [TestMethod]
        public void GetById_GetTestCustomer_ExpextedNotNullValue()
        {
            // Act
            var customer = this.repository.GetById(this.TestCustomer.Id).FirstOrDefault();

            // Assert
            Assert.IsNotNull(customer, "Customer not found");
        }

        [TestMethod]
        public async Task InsertAsync_AddNewCustomerToDb_Expected4CustomersInDb()
        {
            // Arrange
            var newCustomer = this.TestCustomer;
            newCustomer.Id = new Guid();

            // Act
            var result = await repository.InsertAsync(newCustomer);
            var newCustomersCount = context.Set<Customer>().ToList().Count;
            var expectedCount = defaultEntityCount + 1;

            // Assert
            Assert.IsTrue(newCustomersCount == expectedCount, $"Expected {expectedCount} customers in Db, but get {newCustomersCount}");
        }

        [TestMethod]
        public async Task DeleteAsync_DeleteCustomerFromDb_Expected2CustomersInDB()
        {
            // Arrange          

            // Act
            var result = await repository.DeleteAsync(this.TestCustomer.Id);
            var newCustomersCount = context.Set<Customer>().ToList().Count;
            var expectedCount = defaultEntityCount - 1;

            // Assert
            Assert.IsTrue(newCustomersCount == expectedCount, $"Expected {expectedCount} customers in Db, but get {newCustomersCount}");
        }

        [TestMethod]
        public async Task UpdateAsync_UpdateCustomerInfo_ExpectedSuccess()
        {
            // Arrange
            var updatedCustomer = this.TestCustomer;
            updatedCustomer.LastName = "Update";

            // Act
            var result = await repository.UpdateAsync(updatedCustomer);

            // Assert
            Assert.IsTrue(result);
        }
    }
}

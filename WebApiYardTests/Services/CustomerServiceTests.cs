using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.Services;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;
using WebApiYardTests.Stubs;

namespace WebApiYardTests.Services
{
    [TestClass]
    public class CustomerServiceTests
    {
        private ICustomerService service;
        private CustomerServiceModel testCustomer;
        
        [TestInitialize]
        public void TestInitialize()
        {
            this.service = new CustomerService(new CustomerRepositoryStub());
            var repositoryCustomer = TestCustomersCollection.TestCustomer;
            this.testCustomer = new CustomerServiceModel
            {
                Id = repositoryCustomer.Id,
                FirstName = repositoryCustomer.FirstName,
                LastName = repositoryCustomer.LastName,
                Age = repositoryCustomer.Age
            };
            
        }

        [TestMethod]
        public async Task AllAsync_GetAllCustomers_Expected3Customers()
        {
            // Arrange
            var expectedCount = TestCustomersCollection.Collection.Count();

            // Act
            var result = await service.AllAsync();
            var resultCount = result.Count();

            // Assert
            Assert.IsTrue(resultCount == expectedCount, $"Get {resultCount} customers, expected {expectedCount}");
        }

        [TestMethod]
        public async Task GetAsync_GetCustomerInfo_ExpectedNotNullResult()
        {
            // Arrange
            var id = TestCustomersCollection.TestCustomer.Id;

            // Act
            var result = await service.GetAsync(id);

            // Assert
            Assert.IsTrue(result is CustomerServiceModel);
        }

        [TestMethod]
        public async Task SaveAsync_AddNewCustomer_ExpectedNotNull()
        {
            // Arrange

            // Act
            var result = await service.SaveAsync(this.testCustomer);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task UpdateAsync_UpdateCustomerInfo_ExpectedNotNull()
        {
            // Arrange

            // Act
            var result = await this.service.UpdateAsync(this.testCustomer);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task RemoveAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            // Act
            var result = await this.service.RemoveAsync(this.testCustomer.Id);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetCustomerFromDBAsync_GetCustomer_ExpectedCorrectId()
        {
            // Arrange
            var testId = this.testCustomer.Id;

            // Act
            var customer = await this.service.GetCustomerFromDBAsync(testId);

            // Assert
            Assert.IsTrue(testId == customer.Id, $"Expected {testId}, but get {customer.Id}");
        }
    }
}

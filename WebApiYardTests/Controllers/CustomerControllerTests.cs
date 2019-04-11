using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using WebApiYard.Controllers;
using WebApiYard.Controllers.ValidationModels;
using WebApiYard.Mappings;
using WebApiYard.Services.Interfaces;
using WebApiYardTests.Stubs;

namespace WebApiYardTests.Controllers
{
    [TestClass]
    public class CustomerControllerTests
    {
        public CustomerController controller;
        public ICustomerService service;
        public Mapper mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(myProfile));
            this.mapper = new Mapper(configuration);

            this.service = new CustomerServiceStub();
            this.controller = new CustomerController(mapper, service);
        }

        [TestMethod]
        public async Task GetAllAsync_GetCustomerCollection_ExpectedOK()
        {
            // Arrange
            var expectedStatusCode = 200;

            // Act
            var result = await this.controller.GetAllAsync();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.AreEqual(expectedStatusCode, okResult.StatusCode, $"Status code - {okResult.StatusCode}");
        }

        [TestMethod]
        public async Task GetByIdAsync_GetCustomerInfo_ExpectedOK()
        {
            // Arrange
            var expectedStatusCode = 200;
            var customerId = TestCustomersCollection.TestCustomer.Id;

            // Act
            var result = await this.controller.GetByIdAsync(customerId);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.AreEqual(expectedStatusCode, okResult.StatusCode, $"Status code - {okResult.StatusCode}");
        }

        [TestMethod]
        public async Task PostAsync_AddNewCustomer_ExpectedCreatedStatus()
        {
            // Arrange
            var expectedStatusCode = 201;
            var customer = new CustomerCreate
            {
                FirstName = "Name",
                LastName = "Last Name",
                Age = 77
            };

            // Act
            var result = await this.controller.PostAsync(customer);
            var okResult = result as ObjectResult;

            // Assert
            Assert.AreEqual(expectedStatusCode, okResult.StatusCode, $"Status code - {okResult.StatusCode}");
        }

        [TestMethod]
        public async Task PutAsync_UpdateCustomerInfo_ExpectedOK()
        {
            // Arrange
            var expectedStatusCode = 200;
            var id = new Guid();
            var customer = new CustomerUpdate
            {
                Id = id,
                FirstName = "Name",
                LastName = "Last Name",
                Age = 77
            };

            // Act
            var result = await this.controller.PutAsync(id, customer);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.AreEqual(expectedStatusCode, okResult.StatusCode, $"Status code - {okResult.StatusCode}");
        }

        [TestMethod]
        public async Task DeleteAsync_DeleteCustomer_ExpectedNoContent()
        {
            // Arrange
            var expectedStatusCode = 204;
            var id = new Guid();
            
            // Act
            var result = await this.controller.DeleteAsync(id);
            var okResult = result as NoContentResult;

            // Assert
            Assert.AreEqual(expectedStatusCode, okResult.StatusCode, $"Status code - {okResult.StatusCode}");
        }
    }
}

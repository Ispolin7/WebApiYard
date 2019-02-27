using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApiYard.Controllers;
using WebApiYard.Controllers.ViewModels;
using WebApiYard.Mappings;
using WebApiYardTests.Stubs.Services;

namespace WebApiYardTests.Controllers
{
    [TestClass]
    public class CustomerControllerTests
    {
        private CustomerController Controller {get; set;}

        [TestInitialize]
        public void TestInitialize()
        {
            this.Controller = new CustomerController(new CustomerServiceStub());
        }

        
        [TestMethod]
        public void GetAll_StateUnderTest_ExpectedBehavior()
        {
            //
        }

        [TestMethod]
        public void GetById_StateUnderTest_ExpectedBehavior()
        {
            //
        }

        [TestMethod]
        public void Post_StateUnderTest_ExpectedBehavior()
        {
            //
        }

        [TestMethod]
        public void Put_StateUnderTest_ExpectedBehavior()
        {
            //
        }

        [TestMethod]
        public void Delete_StateUnderTest_ExpectedBehavior()
        {
            //
        }
    }
}


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using UnitTests.Stubs;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;

namespace UnitTests.Repositories
{
    [TestClass]
    public class RepositoryTests
    {
        Repository<Customer> TestRepository { get; set; }
        Customer TestCustomer { get; set; }

        /// <summary>
        /// Init repository
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            var repository = new CustomerRepositoryStub();
            this.TestRepository = repository.Customers;
            this.TestCustomer = repository.TestCustomer;
        }

        /// <summary>
        /// Test index method
        /// </summary>
        [TestMethod]
        public void All_GetAllEntities_ExpectedCount3()
        {
            IEnumerable<Customer> collection = TestRepository.All();

            var count = collection.Count();

            Assert.IsTrue(count == 3, $"Emount of elements - {count},expected - 3");
        }

        /// <summary>
        /// Test add new item
        /// </summary>
        [TestMethod]
        public void Insert_AddNewItem_ExpectedCountAs4()
        {
            TestCustomer.Id = Guid.NewGuid();
            this.TestRepository.Insert(TestCustomer);

            var count = Repository<Customer>._entities.Count;

            Assert.IsTrue(count == 4, $"Emount of elements - {count}, expected - 4");
        }

        /// <summary>
        /// Get entity from repository
        /// </summary>
        [TestMethod]
        public void GetById_GetEntity_ExpectedTestCustomerName()
        {
            var customer = TestRepository.GetById(TestCustomer.Id);

            var expectedName = TestCustomer.FirstName;
            var currentName = customer.FirstName;

            Assert.IsTrue(currentName == expectedName, $"Name - {currentName}, expected - {expectedName}");
        }

        /// <summary>
        /// Try to get non-existent Customer
        /// </summary>
        [TestMethod]
        public void GetById_AttempGetNonExistentEntity_ExpectedDefaultCustomer()
        {
            var newGuid = Guid.NewGuid();
            var expectedCustomer = default(Customer);

            var result = TestRepository.GetById(newGuid);

            Assert.IsTrue(result == expectedCustomer);
        }

        /// <summary>
        /// Update entity in repository
        /// </summary>
        [TestMethod]
        public void Update_UpdateEntity_ExpectedSuccess()
        {
            TestCustomer.FirstName = "Update";

            var result = TestRepository.Update(TestCustomer);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Try to update non-existent Customer
        /// </summary>
        [TestMethod]
        public void Update_AttempUpdateNonExistentEntity_ExpectedFalse()
        {
            TestCustomer.Id = Guid.NewGuid();

            var result = TestRepository.Update(TestCustomer);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Remove entity from repository
        /// </summary>
        [TestMethod]
        public void Delete_RemoveEntity_ExpectedCountAs2()
        {
            TestRepository.Delete(TestCustomer.Id);

            var count = Repository<Customer>._entities.Count;

            Assert.IsTrue(count == 2, $"Emount of elements - {count}, expected - 2");
        }

        /// <summary>
        /// Try to remove non-existent Customer
        /// </summary>
        [TestMethod]
        public void Delete_AttemptDeleteNonExistentEntity_ExpectedFalse()
        {
            var result = TestRepository.Delete(Guid.NewGuid());

            Assert.IsFalse(result);
        }
    }
}

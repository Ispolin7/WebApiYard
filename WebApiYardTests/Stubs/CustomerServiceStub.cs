using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApiYard.Mappings;
using WebApiYard.Repositories.Models;
using WebApiYard.Services.Interfaces;
using WebApiYard.Services.Models;

namespace WebApiYardTests.Stubs
{
    class CustomerServiceStub : ICustomerService
    {
        private RepositoryToServiceMapper mapper;

        public CustomerServiceStub()
        {
            this.mapper = new RepositoryToServiceMapper();
        }

        public Task<IEnumerable<CustomerServiceModel>> AllAsync()
        {
            return Task.FromResult(this.mapper.MapCustomers(TestCustomersCollection.Collection));
        }

        public Task<CustomerServiceModel> GetAsync(Guid id)
        {
            return Task.FromResult(this.mapper.MapCustomer(TestCustomersCollection.TestCustomer));
        }

        public Task<bool> RemoveAsync(Guid id)
        {
            return Task.FromResult(true);
        }

        public Task<Guid> SaveAsync(CustomerServiceModel entity)
        {
            return Task.FromResult(new Guid());
        }

        public Task<bool> UpdateAsync(CustomerServiceModel entity)
        {
            return Task.FromResult(true);
        }

        public Task<Customer> GetCustomerFromDBAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

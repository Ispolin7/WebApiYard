using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;

namespace WebApiYardTests.Stubs
{
    public class CustomerRepositoryStub : IRepository<Customer>
    {
        DbContext context;
        DbSet<Customer> dbSet;

        public CustomerRepositoryStub()
        {
            var factory = new TestDbContextFactory();
            this.context = factory.GetContext();
            factory.FillDb(TestCustomersCollection.Collection);
            this.dbSet = context.Set<Customer>();  
        }

        public IQueryable<Customer> All()
        {
            return dbSet
                .AsNoTracking()
                .Where(e => e.IsDelete != true);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            //return new Task<bool>(() => true);
            return Task.FromResult(true);
        }

        public IQueryable<Customer> GetById(Guid id)
        {
            return dbSet
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Where(e => e.IsDelete != true);
        }

        public Task<Guid> InsertAsync(Customer entity)
        {
            // TODO залипает
            //return new Task<Guid>(() => new Guid());
            return Task.FromResult<Guid>(new Guid());
        }

        public Task<bool> UpdateAsync(Customer entity)
        {
            return Task.FromResult(true);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApiYard.DAL;
using WebApiYard.Repositories;

namespace WebApiYardTests
{
    class TestDbContextFactory
    {
        private readonly ApiContext context;

        public TestDbContextFactory()
        {
            var options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: "test_repository")
                .EnableSensitiveDataLogging()
                .Options;

            this.context = new ApiContext(options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool FillDb<T>(IEnumerable<T> collection) where T: class, IEntity<Guid>
        {
            context.Set<T>().RemoveRange(context.Set<T>());
            context.Set<T>().AddRange(collection);
            context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ApiContext GetContext()
        {
            return this.context;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using WebApiYard.Repositories.Models;
using WebApiYard.Repositories.Models.ModelsConfiguration;

namespace WebApiYard.DAL
{
    public class ApiContext : DbContext
    {
        /// <summary>
        /// Constructor. Create db
        /// </summary>
        public ApiContext()
        {
            Database.EnsureCreated();
        }
        
        /// <summary>
        /// Configure connection
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=WebApi;Trusted_Connection=True;ConnectRetryCount=0";
            optionsBuilder.UseSqlServer(connection);
        }

        /// <summary>
        /// Add models configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            // add in db test values
            modelBuilder.Seed();           
        }

        /// <summary>
        /// DbSets
        /// </summary>
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }       
    }
}

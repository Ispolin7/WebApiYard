using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.Repositories.Models;

namespace WebApiYard.DAL
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=WebApi;Trusted_Connection=True;ConnectRetryCount=0";
            optionsBuilder.UseSqlServer(connection);
        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> order_items { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Address> addresses { get; set; }
    }
}

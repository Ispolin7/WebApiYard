using System;
using Microsoft.EntityFrameworkCore;
using WebApiYard.Repositories.Models;

namespace WebApiYard.DAL
{
    public static class ModelBuilderSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = new Guid("2e77d667-ec12-4cca-a582-c950db6e6720"),
                    StreetLine1 = "Chornovola",
                    StreetLine2 = "12",
                    City = "Poltava",
                    State = "Poltava region",
                    PostalCode = "36000",
                    Country = "Ukraine",
                    IsDelete = false,
                    CreatedAT = DateTime.Now
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = new Guid("67b5db3e-ceb2-44f6-e98e-08d6ab94d635"),
                    FirstName = "Second Customer",
                    LastName = "Name",
                    Age = 77,
                    IsDelete = false,
                    CreatedAT = DateTime.Now
                },
                new Customer
                {
                    Id = new Guid("b37f2458-c036-4970-e98d-08d6ab94d635"),
                    FirstName = "First Customer",
                    LastName = "Last Name",
                    Age = 22,
                    IsDelete = false,
                    CreatedAT = DateTime.Now
                }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = new Guid("9458ab9b-6d9e-47a1-1399-08d6ab95178b"),
                    AddressId = new Guid("2e77d667-ec12-4cca-a582-c950db6e6720"),
                    CustomerId = new Guid("b37f2458-c036-4970-e98d-08d6ab94d635"),
                    IsDelete = false,
                    CreatedAT = DateTime.Now
                },
                new Order
                {
                    Id = new Guid("59d823a1-86dd-4243-139a-08d6ab95178b"),
                    AddressId = new Guid("2e77d667-ec12-4cca-a582-c950db6e6720"),
                    CustomerId = new Guid("b37f2458-c036-4970-e98d-08d6ab94d635"),
                    IsDelete = false,
                    CreatedAT = DateTime.Now
                }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = new Guid("82ca39ca-dc44-41f4-1ad3-08d6ab955584"),
                    PurchasePrice = 2000,
                    Quantity = 2,
                    Color = 4,
                    OrderId = new Guid("9458ab9b-6d9e-47a1-1399-08d6ab95178b"),
                    ProductId = new Guid("355da661-541f-46bc-aa8b-08d6ab93d434"),
                    IsDelete = false,
                    CreatedAT = DateTime.Now
                },
                new OrderItem
                {
                    Id = new Guid("27495a67-8210-46df-1ad4-08d6ab955584"),
                    PurchasePrice = 154,
                    Quantity = 2,
                    Color = 4,
                    OrderId = new Guid("9458ab9b-6d9e-47a1-1399-08d6ab95178b"),
                    ProductId = new Guid("a09fede4-0fa0-40de-aa8c-08d6ab93d434"),
                    IsDelete = false,
                    CreatedAT = DateTime.Now
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = new Guid("b8904a27-8040-4760-aa8a-08d6ab93d434"),
                    Name = "First product",
                    Description = "Lorem ipsum dolor sit amet, molestie mauris justo in, commodo fringilla risus metus maecenas porttitor facilisi, pellentesque voluptates fusce. Integer vestibulum arcu ridiculus sed vitae eleifend, sit volutpat, ipsum interdum euismod tempor, sed lectus. Sapien arcu volutpat urna dolor, elit dolor sed pede sollicitudin wisi. Vitae vitae orci a lectus. Iaculis ad enim mi lacinia, faucibus in vel nisl vel suspendisse tincidunt. Placerat lacinia natoque sit praesentium porttitor, pellentesque orci sodales eros blandit, scelerisque urna mi ea imperdiet integer, et neque semper vulputate, aenean in massa tempor vehicula. Neque sed urna nisl inceptos egestas consectetuer. Cum dolor fusce adipiscing in ligula. In pharetra nibh sit quis, porta ornare integer nunc, gravida in morbi mauris. Vitae bibendum felis, et amet ornare fermentum tempus convallis penatibus, risus viverra nulla.",
                    Price = 22,
                    IsDelete = false,
                    CreatedAT = DateTime.Now
                },
                new Product
                {
                    Id = new Guid("355da661-541f-46bc-aa8b-08d6ab93d434"),
                    Name = "Second product",
                    Description = "Lorem ipsum dolor sit amet, molestie mauris justo in, commodo fringilla risus metus maecenas porttitor facilisi, pellentesque voluptates fusce. Integer vestibulum arcu ridiculus sed vitae eleifend, sit volutpat, ipsum interdum euismod tempor, sed lectus. Sapien arcu volutpat urna dolor, elit dolor sed pede sollicitudin wisi. Vitae vitae orci a lectus. Iaculis ad enim mi lacinia, faucibus in vel nisl vel suspendisse tincidunt. Placerat lacinia natoque sit praesentium porttitor, pellentesque orci sodales eros blandit, scelerisque urna mi ea imperdiet integer, et neque semper vulputate, aenean in massa tempor vehicula. Neque sed urna nisl inceptos egestas consectetuer. Cum dolor fusce adipiscing in ligula. In pharetra nibh sit quis, porta ornare integer nunc, gravida in morbi mauris. Vitae bibendum felis, et amet ornare fermentum tempus convallis penatibus, risus viverra nulla.",
                    Price = 33,
                    IsDelete = false,
                    CreatedAT = DateTime.Now
                },
                new Product
                {
                    Id = new Guid("a09fede4-0fa0-40de-aa8c-08d6ab93d434"),
                    Name = "Third product",
                    Description = "Lorem ipsum dolor sit amet, molestie mauris justo in, commodo fringilla risus metus maecenas porttitor facilisi, pellentesque voluptates fusce. Integer vestibulum arcu ridiculus sed vitae eleifend, sit volutpat, ipsum interdum euismod tempor, sed lectus. Sapien arcu volutpat urna dolor, elit dolor sed pede sollicitudin wisi. Vitae vitae orci a lectus. Iaculis ad enim mi lacinia, faucibus in vel nisl vel suspendisse tincidunt. Placerat lacinia natoque sit praesentium porttitor, pellentesque orci sodales eros blandit, scelerisque urna mi ea imperdiet integer, et neque semper vulputate, aenean in massa tempor vehicula. Neque sed urna nisl inceptos egestas consectetuer. Cum dolor fusce adipiscing in ligula. In pharetra nibh sit quis, porta ornare integer nunc, gravida in morbi mauris. Vitae bibendum felis, et amet ornare fermentum tempus convallis penatibus, risus viverra nulla.",
                    Price = 44,
                    IsDelete = false,
                    CreatedAT = DateTime.Now
                }
            );
        }
    }
}

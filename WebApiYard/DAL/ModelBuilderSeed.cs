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
                    Id = Guid.NewGuid(),
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
        }
    }
}

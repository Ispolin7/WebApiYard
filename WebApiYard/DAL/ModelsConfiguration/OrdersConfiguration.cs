using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApiYard.Repositories.Models.ModelsConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            //builder.Ignore(o => o.Items);
            //builder.HasOne<Customer>().WithMany().HasForeignKey(o => o.CustomerId);
            builder.Property(o => o.IsDelete).HasDefaultValue(false);
            builder.Property(o => o.CreatedAT).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        }
    }
}
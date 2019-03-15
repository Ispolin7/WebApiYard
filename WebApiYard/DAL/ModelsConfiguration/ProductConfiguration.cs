using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApiYard.Repositories.Models.ModelsConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.Property(p => p.IsDelete).HasDefaultValue(false);
            builder.Property(o => o.CreatedAT).ValueGeneratedOnAdd().HasDefaultValueSql("GETDATE()");
        }
    }
}
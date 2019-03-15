using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApiYard.Repositories.Models.ModelsConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.Property(a => a.IsDelete).HasDefaultValue(false);
            builder.Ignore(o => o.Orders);
            builder.Property(o => o.CreatedAT).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        }
    }
}

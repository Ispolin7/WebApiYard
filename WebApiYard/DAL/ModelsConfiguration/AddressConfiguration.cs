using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApiYard.Repositories.Models.ModelsConfiguration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.Property(a => a.PostalCode).IsRequired();
            builder.Property(a => a.Country).IsRequired();
            builder.Property(a => a.City).IsRequired();
            builder.Property(a => a.StreetLine1).IsRequired();
            builder.Property(a => a.IsDelete).HasDefaultValue(false);
            builder.Property(a => a.CreatedAT).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        }
    }
}

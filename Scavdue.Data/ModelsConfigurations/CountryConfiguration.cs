using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scavdue.Core.Models;

namespace Scavdue.Data.ModelsConfigurations
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            builder.HasKey(e => e.Id)
                .HasName("Country_pkey");

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Iso3166);

            builder.HasMany(p => p.AdministrativeUnits)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

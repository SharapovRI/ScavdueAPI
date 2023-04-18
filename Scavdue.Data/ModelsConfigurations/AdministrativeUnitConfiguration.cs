using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scavdue.Core.Models;

namespace Scavdue.Data.ModelsConfigurations
{
    internal class AdministrativeUnitConfiguration : IEntityTypeConfiguration<AdministrativeUnit>
    {
        public void Configure(EntityTypeBuilder<AdministrativeUnit> builder)
        {
            builder.ToTable("AdministrativeUnits");

            builder.HasKey(e => e.Id).HasName("AdministrativeUnit_pkey");

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.AdministrativeLevel)
                .IsRequired();

            builder.Property(e => e.ParentAdministrativeUnitId)
                .IsRequired(false);

            builder.Property(e => e.Place)
                .IsRequired(false);

            builder.Property(e => e.CountryId)
                .IsRequired();

            builder.HasOne(p => p.ParentAdministrativeUnit)
                .WithMany(p => p.ChildAdministrativeUnits)
                .HasForeignKey(p => p.ParentAdministrativeUnitId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Country)
                .WithMany(p => p.AdministrativeUnits)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.AdministrativeUnitPolygons)
                .WithOne(p => p.AdministrativeUnit)
                .HasForeignKey(p => p.AdministrativeUnitId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.UnitObjects)
                .WithOne(p => p.AdministrativeUnit)
                .HasForeignKey(p => p.AdministrativeUnitId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Populations)
                .WithOne(p => p.AdministrativeUnit)
                .HasForeignKey(p => p.AdministrativeUnitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

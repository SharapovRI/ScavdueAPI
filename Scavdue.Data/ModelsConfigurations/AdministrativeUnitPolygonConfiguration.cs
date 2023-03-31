using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Scavdue.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Data.ModelsConfigurations
{
    internal class AdministrativeUnitPolygonConfiguration : IEntityTypeConfiguration<AdministrativeUnitPolygon>
    {
        public void Configure(EntityTypeBuilder<AdministrativeUnitPolygon> builder)
        {
            builder.ToTable("AdministrativeUnitPolygons");

            builder.HasKey(e => e.Id)
                .HasName("AdministrativeUnitPolygon_pkey");

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.CenterLong);

            builder.Property(e => e.CenterLat);

            builder.Property(e => e.Coordinates)
                .IsRequired();

            builder.HasOne(p => p.AdministrativeUnit)
                .WithMany(p => p.AdministrativeUnitPolygons)
                .HasForeignKey(p => p.AdministrativeUnitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

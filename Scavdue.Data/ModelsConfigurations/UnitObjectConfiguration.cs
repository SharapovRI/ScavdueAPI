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
    internal class UnitObjectConfiguration : IEntityTypeConfiguration<UnitObject>
    {
        public void Configure(EntityTypeBuilder<UnitObject> builder)
        {
            builder.ToTable("UnitObjects");

            builder.HasKey(e => e.Id)
                .HasName("UnitObject_pkey");

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.HasOne(p => p.AdministrativeUnit)
                .WithMany(p => p.UnitObjects)
                .HasForeignKey(p => p.AdministrativeUnitId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(p => p.UnitObjectType)
                .WithMany(p => p.UnitObjects)
                .HasForeignKey(p => p.UnitObjectTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.UnitObjectPolygons)
                .WithOne(p => p.UnitObject)
                .HasForeignKey(p => p.UnitObjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

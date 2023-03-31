using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scavdue.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavdue.Data.ModelsConfigurations
{
    internal class UnitObjectClassConfiguration : IEntityTypeConfiguration<UnitObjectClass>
    {
        public void Configure(EntityTypeBuilder<UnitObjectClass> builder)
        {
            builder.ToTable("UnitObjectClasses");

            builder.HasKey(e => e.Id)
                .HasName("UnitObjectClass_pkey");

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.HasMany(p => p.UnitObjectTypes)
                .WithOne(p => p.UnitObjectClass)
                .HasForeignKey(p => p.UnitObjectClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

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
    internal class PopulationConfiguration : IEntityTypeConfiguration<Population>
    {
        public void Configure(EntityTypeBuilder<Population> builder)
        {
            builder.ToTable("Populations");

            builder.HasKey(e => e.Id)
                .HasName("Population_pkey");

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.NumberOfPeople)
                .IsRequired();
            
            builder.Property(e => e.Date)
                .IsRequired();

            builder.HasOne(p => p.AdministrativeUnit)
                .WithMany(p => p.Populations)
                .HasForeignKey(p => p.AdministrativeUnitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

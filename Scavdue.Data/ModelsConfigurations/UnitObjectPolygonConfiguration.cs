using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Scavdue.Core.Models;

namespace Scavdue.Data.ModelsConfigurations
{
    internal class UnitObjectPolygonConfiguration : IEntityTypeConfiguration<UnitObjectPolygon>
    {
        public void Configure(EntityTypeBuilder<UnitObjectPolygon> builder)
        {
            builder.ToTable("UnitObjectPolygons");

            builder.HasKey(e => e.Id)
                .HasName("UnitObjectPolygon_pkey");

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.CenterLong);

            builder.Property(e => e.CenterLat);

            builder.Property(e => e.Coordinates)
                .IsRequired();

            builder.HasOne(p => p.UnitObject)
                .WithMany(p => p.UnitObjectPolygons)
                .HasForeignKey(p => p.UnitObjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

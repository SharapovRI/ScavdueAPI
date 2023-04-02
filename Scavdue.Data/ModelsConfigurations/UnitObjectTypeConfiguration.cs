using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scavdue.Core.Models;

namespace Scavdue.Data.ModelsConfigurations
{
    internal class UnitObjectTypeConfiguration : IEntityTypeConfiguration<UnitObjectType>
    {
        public void Configure(EntityTypeBuilder<UnitObjectType> builder)
        {
            builder.ToTable("UnitObjectTypes");

            builder.HasKey(e => e.Id)
                .HasName("UnitObjectType_pkey");

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired();

            builder.HasOne(p => p.UnitObjectClass)
                .WithMany(p => p.UnitObjectTypes)
                .HasForeignKey(p => p.UnitObjectClassId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.UnitObjects)
                .WithOne(p => p.UnitObjectType)
                .HasForeignKey(p => p.UnitObjectTypeId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}

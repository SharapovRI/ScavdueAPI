using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Scavdue.Core.Models;

namespace Scavdue.Data.ModelsConfigurations;

public class LifeIndexConfiguration : IEntityTypeConfiguration<LifeIndex>
{
    public void Configure(EntityTypeBuilder<LifeIndex> builder)
    {
        builder.ToTable("LifeIndexes");

        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(e => e.ReceivingDate)
            .IsRequired();

        builder.Property(e => e.AdministrativeUnitId)
            .IsRequired();

        builder.HasOne(p => p.AdministrativeUnit)
            .WithMany(p => p.LifeIndexes)
            .HasForeignKey(p => p.AdministrativeUnitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.EvaluationCriterias)
            .WithOne(p => p.LifeIndex)
            .HasForeignKey(p => p.LifeIndexId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
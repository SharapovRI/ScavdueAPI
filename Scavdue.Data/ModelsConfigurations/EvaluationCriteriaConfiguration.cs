using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Scavdue.Core.Models;

namespace Scavdue.Data.ModelsConfigurations;

public class EvaluationCriteriaConfiguration : IEntityTypeConfiguration<EvaluationCriteria>
{
    public void Configure(EntityTypeBuilder<EvaluationCriteria> builder)
    {
        builder.ToTable("EvaluationCriterias");
        
        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(e => e.Value)
            .IsRequired();

        builder.Property(e => e.Description)
            .IsRequired(false);

        builder.Property(e => e.LifeIndexId)
            .IsRequired();

        builder.Property(e => e.EvaluationCriteriaTypeId)
            .IsRequired();
        
        builder.HasOne(p => p.EvaluationCriteriaType)
            .WithMany(p => p.EvaluationCriterias)
            .HasForeignKey(p => p.EvaluationCriteriaTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.LifeIndex)
            .WithMany(p => p.EvaluationCriterias)
            .HasForeignKey(d => d.LifeIndexId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
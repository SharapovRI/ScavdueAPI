using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Scavdue.Core.Models;

namespace Scavdue.Data.ModelsConfigurations;

public class EvaluationCriteriaTypeConfiguration : IEntityTypeConfiguration<EvaluationCriteriaType>
{
    public void Configure(EntityTypeBuilder<EvaluationCriteriaType> builder)
    {
        builder.ToTable("EvaluationCriteriaTypes");
        
        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(e => e.Name)
            .IsRequired();

        builder.HasMany(p => p.EvaluationCriterias)
            .WithOne(p => p.EvaluationCriteriaType)
            .HasForeignKey(p => p.EvaluationCriteriaTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
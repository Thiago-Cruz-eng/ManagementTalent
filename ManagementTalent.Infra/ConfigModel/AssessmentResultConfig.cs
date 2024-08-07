using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class AssessmentResultConfig : IEntityTypeConfiguration<AssessmentResult>
{
    public void Configure(EntityTypeBuilder<AssessmentResult> builder)
    {
        builder
            .HasMany(ar => ar.GroupParameterResults)
            .WithOne(gpr => gpr.AssessmentResult);
    }
}
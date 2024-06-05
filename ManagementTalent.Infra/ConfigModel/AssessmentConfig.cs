using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class AssessmentConfig : IEntityTypeConfiguration<Assessment>
{
    public void Configure(EntityTypeBuilder<Assessment> builder)
    {
        builder
            .HasMany(a => a.GroupParameters)
            .WithOne();
    }
}
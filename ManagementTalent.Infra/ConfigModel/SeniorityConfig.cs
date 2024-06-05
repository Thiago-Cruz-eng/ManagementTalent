using ManagementTalent.Domain.Entity.AvaliationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class SeniorityConfig : IEntityTypeConfiguration<Seniority>
{
    public void Configure(EntityTypeBuilder<Seniority> builder)
    {
    }
}
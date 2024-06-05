using ManagementTalent.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class JobRoleConfig : IEntityTypeConfiguration<JobRole>
{
    public void Configure(EntityTypeBuilder<JobRole> builder)
    {
        
    }
}
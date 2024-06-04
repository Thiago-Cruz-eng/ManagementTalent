using ManagementTalent.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Domain.ConfigModel;

public class JobParameterBaseConfig : IEntityTypeConfiguration<JobParameterBase>
{
    public void Configure(EntityTypeBuilder<JobParameterBase> builder)
    {
        
    }
}
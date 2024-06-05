using ManagementTalent.Domain.Entity.AvaliationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class JobParameterBaseConfig : IEntityTypeConfiguration<JobParameterBase>
{
    public void Configure(EntityTypeBuilder<JobParameterBase> builder)
    {
        
    }
}
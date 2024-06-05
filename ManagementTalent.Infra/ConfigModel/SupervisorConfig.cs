using ManagementTalent.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class SupervisorConfig : IEntityTypeConfiguration<Supervisor>
{
    public void Configure(EntityTypeBuilder<Supervisor> builder)
    {
        
    }
}
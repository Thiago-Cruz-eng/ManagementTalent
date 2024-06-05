using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class GroupParameterConfig : IEntityTypeConfiguration<GroupParameter>
{
    public void Configure(EntityTypeBuilder<GroupParameter> builder)
    {
        
    }
}
using ManagementTalent.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Domain.ConfigModel;

public class GroupParameterConfig : IEntityTypeConfiguration<GroupParameter>
{
    public void Configure(EntityTypeBuilder<GroupParameter> builder)
    {
        
    }
}
using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Domain.ConfigModel;

public class GroupParameterConfig : IEntityTypeConfiguration<GroupParameter>
{
    public void Configure(EntityTypeBuilder<GroupParameter> builder)
    {
        builder.HasKey(gp => gp.Id); 

        builder.HasMany(gp => gp.Parameters)
            .WithOne(jpb => jpb.GroupParameter)
            .HasForeignKey(jpb => jpb.Id);
    }
}
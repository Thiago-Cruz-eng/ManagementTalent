using ManagementTalent.Domain.Entity.AvaliationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class GroupParameterJobParameterConfig : IEntityTypeConfiguration<GroupParameterJobParameter>
{
    public void Configure(EntityTypeBuilder<GroupParameterJobParameter> builder)
    {
        builder
            .HasKey(gj => new { gj.GroupParameterId, gj.JobParameterBaseId });
        
        builder
            .HasOne(gj => gj.GroupParameter)
            .WithMany(gp => gp.GroupParameterJobParameters)
            .HasForeignKey(gj => gj.GroupParameterId);
        builder
            .HasOne(gj => gj.JobParameterBase)
            .WithMany(jp => jp.GroupParameterJobParameters)
            .HasForeignKey(gj => gj.JobParameterBaseId);
    }
}
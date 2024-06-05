using ManagementTalent.Domain.Entity.AvaliationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class JobParameterSeniorityConfig : IEntityTypeConfiguration<JobParameterSeniority>
{
    public void Configure(EntityTypeBuilder<JobParameterSeniority> builder)
    {
        builder
            .HasKey(js => new { js.JobParametersBaseId, js.SeniorityId });
        
        builder
            .HasOne(js => js.JobParameterBase)
            .WithMany(jp => jp.JobParameterSeniorities)
            .HasForeignKey(js => js.JobParametersBaseId);
        builder
            .HasOne(js => js.Seniority)
            .WithMany(s => s.JobParameterSeniorities)
            .HasForeignKey(js => js.SeniorityId);    
    }
}
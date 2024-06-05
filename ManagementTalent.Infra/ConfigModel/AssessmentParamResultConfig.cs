using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Domain.ConfigModel;

public class AssessmentParamResultConfig : IEntityTypeConfiguration<AssessmentParamResult>
{
    public void Configure(EntityTypeBuilder<AssessmentParamResult> builder)
    {
        
    }
}
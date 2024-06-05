using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Domain.Entity.ResultContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class GroupParameterResultConfig: IEntityTypeConfiguration<GroupParameterResult>
{
    public void Configure(EntityTypeBuilder<GroupParameterResult> builder)
    {
       
    }
}
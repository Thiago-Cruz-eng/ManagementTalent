using ManagementTalent.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class UserSystemConfig : IEntityTypeConfiguration<UserSystem>
{
    public void Configure(EntityTypeBuilder<UserSystem> builder)
    {
    }
}
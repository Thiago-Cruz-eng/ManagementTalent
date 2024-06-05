using ManagementTalent.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Infra.ConfigModel;

public class ColabConfig : IEntityTypeConfiguration<Colab>
{
    public void Configure(EntityTypeBuilder<Colab> builder)
    { 
        builder
            .HasOne(u => u.Sup)
            .WithMany(p => p.Colabs)
            .HasForeignKey(u => u.Id);
    }
}
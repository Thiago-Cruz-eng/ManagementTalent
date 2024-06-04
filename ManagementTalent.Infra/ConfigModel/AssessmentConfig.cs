using ManagementTalent.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementTalent.Domain.ConfigModel;

public class AssessmentConfig : IEntityTypeConfiguration<Assessment>
{
    public void Configure(EntityTypeBuilder<Assessment> builder)
    {
        builder.HasKey(a => a.Id); 

        builder
            .HasOne(a => a.Collaborator)
            .WithMany() 
            .HasForeignKey(a => a.Id); 

        builder
            .HasMany(a => a.GroupParameters)
            .WithOne(gp => gp.Assessment)
            .HasForeignKey(gp => gp.Id); 
    }
}
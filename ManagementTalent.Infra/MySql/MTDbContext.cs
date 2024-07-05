using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.ConfigModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagementTalent.Infra.MySql
{
    public class MTDbContext : IdentityDbContext<Colab>
    {
        public MTDbContext(DbContextOptions options) : base (options) { }
        
        public DbSet<Colab> Colabs { get; set; }
        public DbSet<UserSystem> UserSystems { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Seniority> Senioritys { get; set; }
        public DbSet<JobRole> JobRoles { get; set; }
        public DbSet<JobParameterBase> JobParameterBases { get; set; }
        public DbSet<GroupParameter> GroupParameters { get; set; }
        public DbSet<GroupParameterResult> GroupParameterResults { get; set; }
        public DbSet<AssessmentResult> AssessmentResults { get; set; }
        public DbSet<AssessmentParamResult> AssessmentParamResults { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<GroupParameterJobParameter> GroupParameterJobParameter { get; set; }
        public DbSet<JobParameterSeniority> JobParameterSeniority { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ColabConfig());
            modelBuilder.ApplyConfiguration(new SupervisorConfig());
            modelBuilder.ApplyConfiguration(new SeniorityConfig());
            modelBuilder.ApplyConfiguration(new JobRoleConfig());
            modelBuilder.ApplyConfiguration(new JobParameterBaseConfig());
            modelBuilder.ApplyConfiguration(new GroupParameterConfig());
            modelBuilder.ApplyConfiguration(new GroupParameterResultConfig());
            modelBuilder.ApplyConfiguration(new AssessmentResultConfig());
            modelBuilder.ApplyConfiguration(new AssessmentParamResultConfig());
            modelBuilder.ApplyConfiguration(new AssessmentConfig());
            modelBuilder.ApplyConfiguration(new GroupParameterJobParameterConfig());
            modelBuilder.ApplyConfiguration(new JobParameterSeniorityConfig());
            modelBuilder.ApplyConfiguration(new UserSystemConfig());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}

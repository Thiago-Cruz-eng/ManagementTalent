using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class JobRoleRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, JobRole>, IJobRoleRepositorySql
{
    public JobRoleRepositorySql(MTDbContext context) : base(context)
    {
    }
}
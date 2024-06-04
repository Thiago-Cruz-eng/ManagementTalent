using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class JobParameterBaseRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, JobParameterBase>, IJobParameterBaseRepositorySql
{
    public JobParameterBaseRepositorySql(MTDbContext context) : base(context)
    {
    }
}
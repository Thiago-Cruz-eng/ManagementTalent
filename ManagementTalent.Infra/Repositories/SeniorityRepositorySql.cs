using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class SeniorityRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, Seniority>, ISeniorityRepositorySql
{
    public SeniorityRepositorySql(MTDbContext context) : base(context)
    {
    }
}
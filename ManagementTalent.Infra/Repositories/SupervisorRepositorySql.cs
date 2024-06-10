using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class SupervisorRepositorySql : EntityFrameworkRepositorySqlAbstract<string, Supervisor>, ISupervisorRepositorySql
{
    public SupervisorRepositorySql(MTDbContext context) : base(context)
    {
    }
}
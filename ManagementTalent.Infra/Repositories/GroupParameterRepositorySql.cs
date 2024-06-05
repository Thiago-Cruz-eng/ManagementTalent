using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class GroupParameterRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, GroupParameter>, IGroupParameterRepositorySql
{
    public GroupParameterRepositorySql(MTDbContext context) : base(context)
    {
    }
}
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class GroupParameterResultRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, GroupParameterResult>, IGroupParameterResultRepositorySql
{
    private MTDbContext _context;
    public GroupParameterResultRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }

    public Task SaveRange(List<GroupParameterResult> groups)
    {
        throw new NotImplementedException();
    }
}
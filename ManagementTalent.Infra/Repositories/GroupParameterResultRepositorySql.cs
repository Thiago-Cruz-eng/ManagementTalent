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

    public async Task SaveRange(List<GroupParameterResult> groups)
    {
        await _context.GroupParameterResults.AddRangeAsync(groups);
    }
}
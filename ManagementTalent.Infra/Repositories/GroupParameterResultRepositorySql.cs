using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<GroupParameterResult>> FindByAssessmentId(string assessmentId)
    {
        return await _context.GroupParameterResults.Where(x => x.AssessmentResultId == assessmentId).ToListAsync();
    }
}
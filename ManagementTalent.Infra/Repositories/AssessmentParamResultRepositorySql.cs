using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class AssessmentParamResultRepositorySql : EntityFrameworkRepositorySqlAbstract<string, AssessmentParamResult>, IAssessmentParamResultRepositorySql
{
    private MTDbContext _context;
    public AssessmentParamResultRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task SaveRange(List<AssessmentParamResult> jobParamBaseToMap)
    {
        await _context.AssessmentParamResults.AddRangeAsync(jobParamBaseToMap);
    }

    public async Task<List<AssessmentParamResult>> GetAssessmentParamResultByGroupParameterResul(Guid groupParameterResultId)
    {
        return _context.AssessmentParamResults.Where(x => x.GroupParameterResultId == groupParameterResultId.ToString()).ToList();
    }
}
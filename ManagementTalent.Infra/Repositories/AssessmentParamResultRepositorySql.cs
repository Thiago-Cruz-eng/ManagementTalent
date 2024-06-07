using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class AssessmentParamResultRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, AssessmentParamResult>, IAssessmentParamResultRepositorySql
{
    private MTDbContext _context;
    public AssessmentParamResultRepositorySql(MTDbContext context) : base(context)
    {
    }

    public Task SaveRange(List<AssessmentParamResult> jobParamBaseToMap)
    {
        throw new NotImplementedException();
    }

    public Task<AssessmentParamResult> GetAssessmentParamResultByGroupParameterResul(Guid groupParameterResultId)
    {
        throw new NotImplementedException();
    }
}
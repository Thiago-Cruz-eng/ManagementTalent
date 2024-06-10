using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IAssessmentParamResultRepositorySql : IBaseRepositorySql<string, AssessmentParamResult>
{
    Task SaveRange(List<AssessmentParamResult> jobParamBaseToMap);
    Task<List<AssessmentParamResult>> GetAssessmentParamResultByGroupParameterResul(Guid groupParameterResultId);
}
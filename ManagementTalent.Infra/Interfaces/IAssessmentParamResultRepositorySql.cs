using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IAssessmentParamResultRepositorySql : IBaseRepositorySql<Guid, AssessmentParamResult>
{
    Task SaveRange(List<AssessmentParamResult> jobParamBaseToMap);
    Task<AssessmentParamResult> GetAssessmentParamResultByGroupParameterResul(Guid groupParameterResultId);
}
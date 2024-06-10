using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IAssessmentResultRepositorySql : IBaseRepositorySql<string, AssessmentResult>
{
    Task<List<AssessmentResult>> GetAssessmentResultByColabId(string colabId);
}
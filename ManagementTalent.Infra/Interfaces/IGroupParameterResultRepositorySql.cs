using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IGroupParameterResultRepositorySql : IBaseRepositorySql<string, GroupParameterResult>
{
    public Task SaveRange(List<GroupParameterResult> groups);
    Task<List<GroupParameterResult>> FindByAssessmentId(string assessmentId);
}
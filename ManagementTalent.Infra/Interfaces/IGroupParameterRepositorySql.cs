using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IGroupParameterRepositorySql : IBaseRepositorySql<Guid, GroupParameter>
{
    public Task<List<GroupParameter>> GetGroupParamsByAssessment(string assessmentId);
    public Task<List<JobParameterBase>> GetJobParameterByGroup(string groupParamId);
}
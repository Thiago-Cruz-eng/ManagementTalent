using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IJobParameterBaseRepositorySql : IBaseRepositorySql<Guid, JobParameterBase>
{
    Task<List<JobParameterBase>> GetActualParamByColabSeniority(List<JobParameterBase> jobParam, string seniorityId);
}
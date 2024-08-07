using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IJobParameterBaseRepositorySql : IBaseRepositorySql<string, JobParameterBase>
{
    Task<List<JobParameterBase>> GetActualJobParamByColabSeniority(List<JobParameterBase> jobParam, string seniorityId);
}
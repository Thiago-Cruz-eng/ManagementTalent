using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IJobParameterBaseRepositorySql : IBaseRepositorySql<Guid, JobParameterBase>
{
    
}
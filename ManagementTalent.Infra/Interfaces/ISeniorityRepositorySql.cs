using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface ISeniorityRepositorySql : IBaseRepositorySql<Guid, Seniority>
{
    
}
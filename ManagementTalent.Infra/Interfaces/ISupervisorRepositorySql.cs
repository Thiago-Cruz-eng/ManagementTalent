using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface ISupervisorRepositorySql : IBaseRepositorySql<string, Supervisor>
{
    Task<Supervisor> GetSupervisorByName(string name);
}
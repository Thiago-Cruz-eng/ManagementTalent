using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IUserSystemRepositorySql : IBaseRepositorySql<string, UserSystem>
{
    
}
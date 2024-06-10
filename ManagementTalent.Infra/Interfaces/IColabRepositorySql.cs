using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IColabRepositorySql : IBaseRepositorySql<string, Colab>
{
    
}
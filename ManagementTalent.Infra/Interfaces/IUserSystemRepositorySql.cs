using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;

namespace ManagementTalent.Infra.Interfaces;

public interface IUserSystemRepositorySql : IBaseRepositorySql<string, UserSystem>
{
    Task<UserSystem> GetLogin(string email, string password);
}
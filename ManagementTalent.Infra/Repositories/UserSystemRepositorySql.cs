using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class UserSystemRepositorySql : EntityFrameworkRepositorySqlAbstract<string, UserSystem>, IUserSystemRepositorySql
{
    private readonly MTDbContext _context;
    public UserSystemRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<UserSystem> GetLogin(string email, string password)
    {
        return _context.UserSystems.First(x => x.Email == email && x.Password == password);
    }
}
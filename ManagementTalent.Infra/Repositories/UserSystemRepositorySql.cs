using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class UserSystemRepositorySql : EntityFrameworkRepositorySqlAbstract<string, UserSystem>, IUserSystemRepositorySql
{
    public UserSystemRepositorySql(MTDbContext context) : base(context)
    {
    }
}
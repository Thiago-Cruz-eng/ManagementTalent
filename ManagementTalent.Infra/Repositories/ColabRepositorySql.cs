using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class ColabRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, Colab>, IColabRepositorySql
{
    public ColabRepositorySql(MTDbContext context) : base(context)
    {
    }
}
using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class AssessmentRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, Assessment>, IAssessmentRepositorySql
{
    public AssessmentRepositorySql(MTDbContext context) : base(context)
    {
    }
}
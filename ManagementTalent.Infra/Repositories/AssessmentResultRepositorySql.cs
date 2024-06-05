using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class AssessmentResultRepositorySql  : EntityFrameworkRepositorySqlAbstract<Guid, AssessmentResult>, IAssessmentResultRepositorySql
{
    public AssessmentResultRepositorySql(MTDbContext context) : base(context)
    {
    }
}
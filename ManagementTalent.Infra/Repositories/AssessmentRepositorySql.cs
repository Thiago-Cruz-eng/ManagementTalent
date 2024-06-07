using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class AssessmentRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, Assessment>, IAssessmentRepositorySql
{
    private MTDbContext _context;
    public AssessmentRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<Assessment> GetAssessmentByJobRole(string jobRoleId)
    {
        throw new NotImplementedException();
    }
}
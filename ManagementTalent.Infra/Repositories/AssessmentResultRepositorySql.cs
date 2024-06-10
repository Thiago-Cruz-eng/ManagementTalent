using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;
using Microsoft.EntityFrameworkCore;

namespace ManagementTalent.Infra.Repositories;

public class AssessmentResultRepositorySql  : EntityFrameworkRepositorySqlAbstract<string, AssessmentResult>, IAssessmentResultRepositorySql
{
    private MTDbContext _context;
    public AssessmentResultRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<AssessmentResult>> GetAssessmentResultByColabId(string colabId)
    {
        return await _context.AssessmentResults.Where(x => x.CollaboratorId == colabId).ToListAsync();
    }
}
using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;
using Microsoft.EntityFrameworkCore;

namespace ManagementTalent.Infra.Repositories;

public class SeniorityRepositorySql : EntityFrameworkRepositorySqlAbstract<string, Seniority>, ISeniorityRepositorySql
{
    private MTDbContext _context;
    public SeniorityRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Seniority>> GetAssessmentByJobRole(string jobRoleId)
    {
        return await _context.Senioritys.Where(x => x.JobRoleId.ToString() == jobRoleId).ToListAsync();
    }
}
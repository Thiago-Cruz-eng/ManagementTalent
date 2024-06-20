using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;
using Microsoft.EntityFrameworkCore;

namespace ManagementTalent.Infra.Repositories;

public class JobRoleRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, JobRole>, IJobRoleRepositorySql
{
    private readonly MTDbContext _context;
    public JobRoleRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<JobRole> GetJobRoleByName(string name)
    {
        return await _context.JobRoles.Where(x => x.JobTitle == name).FirstOrDefaultAsync();
    }
}
using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;
using Microsoft.EntityFrameworkCore;

namespace ManagementTalent.Infra.Repositories;

public class SupervisorRepositorySql : EntityFrameworkRepositorySqlAbstract<string, Supervisor>, ISupervisorRepositorySql
{
    private readonly MTDbContext _context;
    public SupervisorRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Supervisor> GetSupervisorByName(string name)
    {
        return await _context.Supervisors.Where(x => x.Name == name).FirstOrDefaultAsync();
    }
}
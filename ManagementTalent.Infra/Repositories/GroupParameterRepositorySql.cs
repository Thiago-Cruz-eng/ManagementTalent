using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class GroupParameterRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, GroupParameter>, IGroupParameterRepositorySql
{
    private MTDbContext _context;
    public GroupParameterRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<GroupParameter>> GetGroupParamsByAssessment(string assessmentId)
    {
        throw new NotImplementedException();

    }

    public Task<List<JobParameterBase>> GetJobParameterByGroup(string groupParamId)
    {
        throw new NotImplementedException();
    }
}
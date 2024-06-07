using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;

namespace ManagementTalent.Infra.Repositories;

public class JobParameterBaseRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, JobParameterBase>, IJobParameterBaseRepositorySql
{
    private MTDbContext _context;
    public JobParameterBaseRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }

    public Task SaveRange(List<AssessmentParamResult> groups)
    {
        throw new NotImplementedException();
    }

    public Task<List<JobParameterBase>> GetActualParamByColabSeniority(List<JobParameterBase> jobParam, string seniorityId)
    {
        throw new NotImplementedException();
    }
}
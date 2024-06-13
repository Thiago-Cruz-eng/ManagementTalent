using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<JobParameterBase>> GetActualJobParamByColabSeniority(List<JobParameterBase> jobParam, string seniorityId)
    {
        var job = new List<string>();
        foreach (var jobParameterBase in jobParam)
        {
            job.AddRange(await _context.JobParameterSeniority
                .Where(x => x.SeniorityId == seniorityId && x.JobParametersBaseId == jobParameterBase.Id)
                .Select(x => x.JobParametersBaseId)
                .ToListAsync());

        }

        return await _context.JobParameterBases
            .Where(param => job.Contains(param.Id))
            .ToListAsync();
    }
}
using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.BaseRepository;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Infra.MySql;
using Microsoft.EntityFrameworkCore;

namespace ManagementTalent.Infra.Repositories;

public class GroupParameterRepositorySql : EntityFrameworkRepositorySqlAbstract<Guid, GroupParameter>, IGroupParameterRepositorySql
{
    private MTDbContext _context;
    public GroupParameterRepositorySql(MTDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<GroupParameter>> GetGroupParamsByAssessment(string assessmentId)
    {
        return _context.GroupParameters.Where(x => x.AssessmentId == assessmentId).ToList();
    }

    public async Task<List<JobParameterBase>> GetJobParameterByGroup(string groupParamId)
    {
        var jobParamIds = await _context.GroupParameterJobParameter
            .Where(x => x.GroupParameterId == groupParamId)
            .Select(x => x.JobParameterBaseId)
            .ToListAsync();
        
        var jobParams = await _context.JobParameterBases
            .Where(param => jobParamIds.Contains(param.Id))
            .ToListAsync();

        return jobParams;
    }
}
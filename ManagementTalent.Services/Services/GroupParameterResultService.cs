using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.Interfaces;

namespace ManagementTalent.Services.Services;

public class GroupParameterResultService
{
    private readonly IGroupParameterResultRepositorySql _groupParameterResultRepositorySql;
    private readonly IAssessmentResultRepositorySql _assessmentResultRepositorySql;

    public GroupParameterResultService(IGroupParameterResultRepositorySql groupParameterResultRepositorySql, IAssessmentResultRepositorySql assessmentResultRepositorySql)
    {
        _groupParameterResultRepositorySql = groupParameterResultRepositorySql;
        _assessmentResultRepositorySql = assessmentResultRepositorySql;
    }

    public async Task<GroupParameterResult> CreateGroupParameterResult(GroupParameterResult groupParameterResultDto)
    {
        await _groupParameterResultRepositorySql.Save(groupParameterResultDto);
        await _groupParameterResultRepositorySql.SaveChange();
        return groupParameterResultDto;
    }
    
    public async Task<GroupParameterResult> UpdateGroupParameterResult(Guid id, GroupParameterResult groupParameterResultDto)
    {
        var groupParameterResult = await _groupParameterResultRepositorySql.FindById(id);
        if (groupParameterResult == null) throw new ApplicationException("exercise not found");
 
        await _groupParameterResultRepositorySql.Update(groupParameterResultDto);
        await _groupParameterResultRepositorySql.SaveChange();
        return groupParameterResultDto;
    }
    
    public async Task<GroupParameterResult> GetGroupParameterResult(Guid id)
    {
        var groupParameterResult = await _groupParameterResultRepositorySql.FindById(id);
        return groupParameterResult;
    }
    
    public async Task<List<GroupParameterResult>> GetGroupParameterByAssessmentResult(Guid assessmentResultId)
    {
        var assessmentResult = await _assessmentResultRepositorySql.FindById(assessmentResultId.ToString());
        var groups = await _groupParameterResultRepositorySql.FindAll();
        var groupsByAssessmentResult = groups
            .Select(x => x.AssessmentResultId)
            .Where(assessmentId => assessmentResult.Id == assessmentId)
            .ToList();
        var group = new List<GroupParameterResult>();
        foreach (var id in groupsByAssessmentResult)
        {
            group.Add(await GetGroupParameterResult(Guid.Parse(id)));
        }
        return group;
    }

    public async Task<List<GroupParameterResult>> GetAllGroupParameterResult()
    {
        return await _groupParameterResultRepositorySql.FindAll();
    }
    
    public async Task DeleteGroupParameterResultById(Guid id)
    {
        var groupParameterResult = await _groupParameterResultRepositorySql.FindById(id);
        _groupParameterResultRepositorySql.Delete(groupParameterResult);
        await _groupParameterResultRepositorySql.SaveChange();
    }
}
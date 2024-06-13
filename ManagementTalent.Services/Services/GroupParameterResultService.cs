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
    
    public async Task<GroupParameterResult> UpdateGroupParameterResult(string id, GroupParameterResult groupParameterResultDto)
    {
        var groupParameterResult = await _groupParameterResultRepositorySql.FindById(id);
        if (groupParameterResult == null) throw new ApplicationException("exercise not found");
 
        await _groupParameterResultRepositorySql.Update(groupParameterResultDto);
        await _groupParameterResultRepositorySql.SaveChange();
        return groupParameterResultDto;
    }
    
    public async Task<GroupParameterResult> GetGroupParameterResult(string id)
    {
        var groupParameterResult = await _groupParameterResultRepositorySql.FindById(id);
        return groupParameterResult;
    }
    
    public async Task<List<GroupParameterResult>> GetGroupParameterByAssessmentResult(string assessmentResultId)
    {
        return await _groupParameterResultRepositorySql.FindByAssessmentId(assessmentResultId);
    }

    public async Task<List<GroupParameterResult>> GetAllGroupParameterResult()
    {
        return await _groupParameterResultRepositorySql.FindAll();
    }
    
    public async Task DeleteGroupParameterResultById(string id)
    {
        var groupParameterResult = await _groupParameterResultRepositorySql.FindById(id);
        _groupParameterResultRepositorySql.Delete(groupParameterResult);
        await _groupParameterResultRepositorySql.SaveChange();
    }
}
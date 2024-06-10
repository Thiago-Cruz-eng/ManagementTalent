using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;
using UpdateAssessmentParamResultRequest = ManagementTalent.Services.Services.Dtos.Requests.UpdateAssessmentParamResultRequest;

namespace ManagementTalent.Services.Services;

public class AssessmentParamResultService
{
    private readonly IAssessmentParamResultRepositorySql _assessmentParamResultRepositorySql;
    private readonly IGroupParameterResultRepositorySql _groupParameterResultRepositorySql;

    public AssessmentParamResultService(IAssessmentParamResultRepositorySql assessmentParamResultRepositorySql, IGroupParameterResultRepositorySql groupParameterResultRepositorySql)
    {
        _assessmentParamResultRepositorySql = assessmentParamResultRepositorySql;
        _groupParameterResultRepositorySql = groupParameterResultRepositorySql;
    }

    public async Task<CreateAssessmentParamResultResponse> CreateAssessmentParamResult(CreateAssessmentParamResultRequest assessmentParamResultDto)
    {
        var assessmentParamResult = new AssessmentParamResult
        {
            Description = assessmentParamResultDto.Description,
            Observation = assessmentParamResultDto.Observation,
            RealityResult = assessmentParamResultDto.Result
        };
        
        assessmentParamResult.Validate();
 
        await _assessmentParamResultRepositorySql.Save(assessmentParamResult);
        await _assessmentParamResultRepositorySql.SaveChange();
        return new CreateAssessmentParamResultResponse
        {
            Description = assessmentParamResult.Description,
            Observation = assessmentParamResult.Observation,
            Result = assessmentParamResult.RealityResult ?? 0
        };
    }
    
    public async Task<UpdateAssessmentParamResultResponse> UpdateAssessmentParamResult(Guid id, UpdateAssessmentParamResultRequest assessmentParamResultDto)
    {
        var assessmentParamResult = await _assessmentParamResultRepositorySql.FindById(id.ToString());
        if (assessmentParamResult == null) throw new ApplicationException("exercise not found");
        assessmentParamResult.Description = assessmentParamResultDto.Description ?? assessmentParamResult.Description;
        assessmentParamResult.Observation = assessmentParamResultDto.Observation ?? assessmentParamResult.Observation;
        assessmentParamResult.RealityResult = assessmentParamResultDto.Result ?? assessmentParamResult.RealityResult;    
        
        assessmentParamResult.Validate();
 
        await _assessmentParamResultRepositorySql.Update(assessmentParamResult);
        await _assessmentParamResultRepositorySql.SaveChange();
        return new UpdateAssessmentParamResultResponse
        {
            Success = true
        };
    }
    
    public async Task<GetAssessmentParamResultResponse> GetAssessmentParamResult(Guid id)
    {
        var assessmentParamResult = await _assessmentParamResultRepositorySql.FindById(id.ToString());
        return new GetAssessmentParamResultResponse
        {
            Description = assessmentParamResult.Description,
            Observation = assessmentParamResult.Observation,
            Result = assessmentParamResult.RealityResult ?? 0
        };
    }
    
    private async Task<List<AssessmentParamResult>> GetAssessmentParamResultByGroupParameterResultId(Guid groupParameterResultId)
    {
        var assessmentParamResult = await _assessmentParamResultRepositorySql.GetAssessmentParamResultByGroupParameterResul(groupParameterResultId);
        return assessmentParamResult;
    }
    
    public async Task<List<AssessmentParamResult>> GetAssessmentParamByGroupResult(List<Guid> groupParamIds)
    {
        var jobParams = new List<AssessmentParamResult>();
        foreach (var groupParamId in groupParamIds)
        {
            jobParams.AddRange(await GetAssessmentParamResultByGroupParameterResultId(groupParamId));
        }
        
        return jobParams;
    }

    public async Task<List<GetAssessmentParamResultResponse>> GetAllAssessmentParamResult()
    {
        var assessmentParamResultResponses = new List<GetAssessmentParamResultResponse>();
        var assessmentParamResult = await _assessmentParamResultRepositorySql.FindAll();
        assessmentParamResult.ForEach(x =>
        {
            assessmentParamResultResponses.Add(new GetAssessmentParamResultResponse
            {
                Description = x.Description,
                Observation = x.Observation,
                Result = x.RealityResult ?? 0
            });
        });
        return assessmentParamResultResponses;
    }
    
    public async Task DeleteAssessmentParamResultById(Guid id)
    {
        var assessmentParamResult = await _assessmentParamResultRepositorySql.FindById(id.ToString());
        _assessmentParamResultRepositorySql.Delete(assessmentParamResult);
        await _assessmentParamResultRepositorySql.SaveChange();
    }
}
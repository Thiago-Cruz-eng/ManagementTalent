using ManagementTalent.Domain.Entity;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;
using UpdateAssessmentParamResultRequest = ManagementTalent.Services.Services.Dtos.Requests.UpdateAssessmentParamResultRequest;

namespace ManagementTalent.Services.Services;

public class AssessmentParamResultService
{
    private readonly IAssessmentParamResultRepositorySql _assessmentParamResultRepositorySql;

    public AssessmentParamResultService(IAssessmentParamResultRepositorySql assessmentParamResultRepositorySql)
    {
        _assessmentParamResultRepositorySql = assessmentParamResultRepositorySql;
    }

    public async Task<CreateAssessmentParamResultResponse> CreateAssessmentParamResult(CreateAssessmentParamResultRequest assessmentParamResultDto)
    {
        var assessmentParamResult = new AssessmentParamResult
        {
            Description = assessmentParamResultDto.Description,
            Observation = assessmentParamResultDto.Observation,
            Result = assessmentParamResultDto.Result
        };
        
        assessmentParamResult.Validate();
 
        await _assessmentParamResultRepositorySql.Save(assessmentParamResult);
        await _assessmentParamResultRepositorySql.SaveChange();
        return new CreateAssessmentParamResultResponse
        {
            Description = assessmentParamResult.Description,
            Observation = assessmentParamResult.Observation,
            Result = assessmentParamResult.Result
        };
    }
    
    public async Task<UpdateAssessmentParamResultResponse> UpdateAssessmentParamResult(Guid id, UpdateAssessmentParamResultRequest assessmentParamResultDto)
    {
        var assessmentParamResult = await _assessmentParamResultRepositorySql.FindById(id);
        if (assessmentParamResult == null) throw new ApplicationException("exercise not found");
        assessmentParamResult.Description = assessmentParamResultDto.Description ?? assessmentParamResult.Description;
        assessmentParamResult.Observation = assessmentParamResultDto.Observation ?? assessmentParamResult.Observation;
        assessmentParamResult.Result = assessmentParamResultDto.Result ?? assessmentParamResult.Result;    
        
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
        var assessmentParamResult = await _assessmentParamResultRepositorySql.FindById(id);
        return new GetAssessmentParamResultResponse
        {
            Description = assessmentParamResult.Description,
            Observation = assessmentParamResult.Observation,
            Result = assessmentParamResult.Result
        };
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
                Result = x.Result
            });
        });
        return assessmentParamResultResponses;
    }
    
    public async Task DeleteAssessmentParamResultById(Guid id)
    {
        var assessmentParamResult = await _assessmentParamResultRepositorySql.FindById(id);
        _assessmentParamResultRepositorySql.Delete(assessmentParamResult);
        await _assessmentParamResultRepositorySql.SaveChange();
    }
}
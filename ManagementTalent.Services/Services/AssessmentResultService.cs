using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class AssessmentResultService
{
    private readonly IAssessmentResultRepositorySql _assessmentResultRepositorySql;

    public AssessmentResultService(IAssessmentResultRepositorySql assessmentResultRepositorySql)
    {
        _assessmentResultRepositorySql = assessmentResultRepositorySql;
    }

    public async Task<CreateAssessmentResultResponse> CreateAssessmentResult(CreateAssessmentResultRequest assessmentResultDto)
    {
        var assessmentResult = new AssessmentResult
        {
            Collaborator = assessmentResultDto.Collaborator,
            GroupParameterResults = assessmentResultDto.GroupParameterResults,
            Result = assessmentResultDto.Result,
            NextAssessment = DateTime.UtcNow.AddYears(1)
        };
        
        assessmentResult.Validate();
 
        await _assessmentResultRepositorySql.Save(assessmentResult);
        await _assessmentResultRepositorySql.SaveChange();
        return new CreateAssessmentResultResponse
        {
            Collaborator = assessmentResult.Collaborator,
            Result = assessmentResult.Result,
            NextAssessment = assessmentResult.NextAssessment
        };
    }
    
    public async Task<UpdateAssessmentResultResponse> UpdateAssessmentResult(Guid id, UpdateAssessmentResultRequest assessmentResultDto)
    {
        var assessmentResult = await _assessmentResultRepositorySql.FindById(id);
        if (assessmentResult == null) throw new ApplicationException("exercise not found");
        assessmentResult.Collaborator = assessmentResultDto.Collaborator ?? assessmentResult.Collaborator;
        assessmentResult.GroupParameterResults = assessmentResultDto.GroupParameterResults ?? assessmentResult.GroupParameterResults;
        assessmentResult.Result = assessmentResultDto.Result ?? assessmentResult.Result;    
        
        assessmentResult.Validate();
 
        await _assessmentResultRepositorySql.Update(assessmentResult);
        await _assessmentResultRepositorySql.SaveChange();
        return new UpdateAssessmentResultResponse
        {
            Success = true
        };
    }
    
    public async Task<GetAssessmentResultResponse> GetAssessmentResult(Guid id)
    {
        var assessmentResult = await _assessmentResultRepositorySql.FindById(id);
        return new GetAssessmentResultResponse
        {
            Collaborator = assessmentResult.Collaborator,
            GroupParameterResults =  assessmentResult.GroupParameterResults,
            Result = assessmentResult.Result,
            NextAssessment = assessmentResult.NextAssessment
        };
    }

    public async Task<List<GetAssessmentResultResponse>> GetAllAssessmentResult()
    {
        var assessmentResultResponses = new List<GetAssessmentResultResponse>();
        var assessmentResult = await _assessmentResultRepositorySql.FindAll();
        assessmentResult.ForEach(x =>
        {
            assessmentResultResponses.Add(new GetAssessmentResultResponse
            {
                Collaborator = x.Collaborator,
                GroupParameterResults = x.GroupParameterResults,
                Result = x.Result,
                NextAssessment = x.NextAssessment
            });
        });
        return assessmentResultResponses;
    }
    
    public async Task DeleteAssessmentResultById(Guid id)
    {
        var assessmentResult = await _assessmentResultRepositorySql.FindById(id);
        _assessmentResultRepositorySql.Delete(assessmentResult);
        await _assessmentResultRepositorySql.SaveChange();
    }
}
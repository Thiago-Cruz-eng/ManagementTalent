using ManagementTalent.Domain.Entity;
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
            AssessmentParam = assessmentResultDto.AssessmentParam,
            Description = assessmentResultDto.Description,
            Observation = assessmentResultDto.Observation,
            SupervisorName = assessmentResultDto.SupervisorName,
            Result = assessmentResultDto.Result
        };
        
        assessmentResult.Validate();
 
        await _assessmentResultRepositorySql.Save(assessmentResult);
        await _assessmentResultRepositorySql.SaveChange();
        return new CreateAssessmentResultResponse
        {
            Collaborator = assessmentResult.Collaborator,
            AssessmentParam = assessmentResult.AssessmentParam,
            Description = assessmentResult.Description,
            Observation = assessmentResult.Observation,
            SupervisorName = assessmentResult.SupervisorName,
            Result = assessmentResult.Result
        };
    }
    
    public async Task<UpdateAssessmentResultResponse> UpdateAssessmentResult(Guid id, UpdateAssessmentResultRequest assessmentResultDto)
    {
        var assessmentResult = await _assessmentResultRepositorySql.FindById(id);
        if (assessmentResult == null) throw new ApplicationException("exercise not found");
        assessmentResult.Collaborator = assessmentResultDto.Collaborator ?? assessmentResult.Collaborator;
        assessmentResult.AssessmentParam = assessmentResultDto.Type ?? assessmentResult.AssessmentParam;
        assessmentResult.Description = assessmentResultDto.Description ?? assessmentResult.Description;
        assessmentResult.Observation = assessmentResultDto.Observation ?? assessmentResult.Observation;
        assessmentResult.SupervisorName = assessmentResultDto.SupervisorName ?? assessmentResult.SupervisorName;
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
            AssessmentParam = assessmentResult.AssessmentParam,
            Description = assessmentResult.Description,
            Observation = assessmentResult.Observation,
            SupervisorName = assessmentResult.SupervisorName,
            Result = assessmentResult.Result
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
                AssessmentParam = x.AssessmentParam,
                Description = x.Description,
                Observation = x.Observation,
                SupervisorName = x.SupervisorName,
                Result = x.Result
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
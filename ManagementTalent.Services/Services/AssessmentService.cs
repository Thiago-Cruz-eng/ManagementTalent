using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;
using ManagementTalent.Infra.Interfaces;
using ManagementTalent.Services.Services.Dtos.Requests;
using ManagementTalent.Services.Services.Dtos.Response;

namespace ManagementTalent.Services.Services;

public class AssessmentService
{
    private readonly IAssessmentRepositorySql _assessmentRepositorySql;

    public AssessmentService(IAssessmentRepositorySql assessmentRepositorySql)
    {
        _assessmentRepositorySql = assessmentRepositorySql;
    }

    public async Task<CreateAssessmentResponse> CreateAssessment(CreateAssessmentRequest assessmentDto)
    {
        var assessment = new Assessment
        {
            Collaborator = assessmentDto.Collaborator,
            GroupParameters = assessmentDto.GroupParameters,
        };
        
        assessment.Validate();
 
        await _assessmentRepositorySql.Save(assessment);
        await _assessmentRepositorySql.SaveChange();
        return new CreateAssessmentResponse
        {
            Collaborator = assessment.Collaborator,
            GroupParameters = assessment.GroupParameters,
        };
    }
    
    public async Task<UpdateAssessmentResponse> UpdateAssessment(Guid id, UpdateAssessmentRequest assessmentDto)
    {
        var assessment = await _assessmentRepositorySql.FindById(id);
        if (assessment == null) throw new ApplicationException("exercise not found");
        assessment.Collaborator = assessmentDto.Collaborator ?? assessment.Collaborator;
        assessment.GroupParameters = assessmentDto.GroupParameters ?? assessment.GroupParameters;   
        
        assessment.Validate();
 
        await _assessmentRepositorySql.Update(assessment);
        await _assessmentRepositorySql.SaveChange();
        return new UpdateAssessmentResponse
        {
            Success = true
        };
    }
    
    public async Task<GetAssessmentResponse> GetAssessment(Guid id)
    {
        var assessment = await _assessmentRepositorySql.FindById(id);
        return new GetAssessmentResponse
        {
            Collaborator = assessment.Collaborator,
            GroupParameters = assessment.GroupParameters,
        };
    }

    public async Task<List<GetAssessmentResponse>> GetAllAssessment()
    {
        var assessmentResponses = new List<GetAssessmentResponse>();
        var assessment = await _assessmentRepositorySql.FindAll();
        assessment.ForEach(x =>
        {
            assessmentResponses.Add(new GetAssessmentResponse
            {
                Collaborator = x.Collaborator,
                GroupParameters = x.GroupParameters,
            });
        });
        return assessmentResponses;
    }
    
    public async Task DeleteAssessmentById(Guid id)
    {
        var assessment = await _assessmentRepositorySql.FindById(id);
        _assessmentRepositorySql.Delete(assessment);
        await _assessmentRepositorySql.SaveChange();
    }
}
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
            JobRoleId = assessmentDto.JobRoleId
        };
        
        assessment.Validate();
 
        await _assessmentRepositorySql.Save(assessment);
        await _assessmentRepositorySql.SaveChange();
        return new CreateAssessmentResponse
        {
            Id = assessment.Id,
            JobRoleId = assessment.JobRoleId
        };
    }
    
    public async Task<UpdateAssessmentResponse> UpdateAssessment(Guid id, UpdateAssessmentRequest assessmentDto)
    {
        var assessment = await _assessmentRepositorySql.FindById(id);
        if (assessment == null) throw new ApplicationException("exercise not found");
        assessment.JobRoleId = assessmentDto.JobRoleId ?? assessment.JobRoleId;
        
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
            Id = assessment.Id,
            JobRoleId = assessment.JobRoleId
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
                Id = x.Id,
                JobRoleId = x.JobRoleId
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
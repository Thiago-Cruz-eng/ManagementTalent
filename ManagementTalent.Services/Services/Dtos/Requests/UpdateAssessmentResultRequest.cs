using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateAssessmentResultRequest
{
    public Colab? Collaborator { get; set; }
    public List<AssessmentParamResult>? Type { get; set; }
    public string? Description { get; set; }
    public string? Observation { get; set; }
    public string? SupervisorName { get; set; }
    public int? Result { get; set; }
}
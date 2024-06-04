using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetAssessmentResultResponse
{
    public Colab Collaborator { get; set; }
    public List<AssessmentParamResult> AssessmentParam { get; set; }
    public string Description { get; set; }
    public string Observation { get; set; }
    public string SupervisorName { get; set; }
    public int Result { get; set; }
}
namespace ManagementTalent.Domain.Entity.ResultContext;

public class GroupParameterResult
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string GroupParamTitle { get; set; }
    public double Weight { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public string AssessmentResultId { get; set; }
    public string AssessmentTamplateId { get; set; }
    public string ActualSeniorityId { get; set; }
    public List<AssessmentParamResult> AssessmentParam { get; set; }
    public AssessmentResult AssessmentResult { get; set; }
}
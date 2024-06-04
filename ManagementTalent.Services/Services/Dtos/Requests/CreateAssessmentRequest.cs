using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateAssessmentRequest
{
    public Colab Collaborator { get; set; }
    public DateTime? NextAssessment { get; set; } = DateTime.Now.AddYears(1);
    public List<GroupParameter> GroupParameters { get; set; }
}
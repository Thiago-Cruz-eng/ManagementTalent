using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetAssessmentResponse
{
    public Colab Collaborator { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime? NextAssessment { get; set; } = DateTime.Now.AddYears(1);
    public List<GroupParameter> GroupParameters { get; set; }
}
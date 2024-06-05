using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class CreateAssessmentResponse
{
    public Colab Collaborator { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime? NextAssessment { get; set; } = DateTime.Now.AddYears(1);
    public ICollection<GroupParameter> GroupParameters { get; set; }
}
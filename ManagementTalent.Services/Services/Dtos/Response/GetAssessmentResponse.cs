using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetAssessmentResponse
{
    public Colab Collaborator { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public ICollection<GroupParameter> GroupParameters { get; set; }
}
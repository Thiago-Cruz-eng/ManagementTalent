using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateAssessmentRequest
{
    public Colab Collaborator { get; set; }
    public List<GroupParameter> GroupParameters { get; set; }
}
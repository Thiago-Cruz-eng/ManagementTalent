using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateAssessmentRequest
{
    public Guid? JobRoleId { get; set; }
    public DateTime? CreateAt { get; set; } = DateTime.Now;
    public List<GroupParameter>? GroupParameters { get; set; }
}
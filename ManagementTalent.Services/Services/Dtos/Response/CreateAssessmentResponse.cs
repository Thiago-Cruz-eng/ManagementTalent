using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class CreateAssessmentResponse
{
    public string Id { get; set; }
    public Guid JobRoleId { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
}
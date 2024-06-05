using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateColabRequest
{
    public string Name { get; set; }
    public DateTime StartAt { get; set; }

    public Guid JobRoleId { get; set; }
    public Guid SeniorityId { get; set; }
    public string SupervisorId { get; set; }
}
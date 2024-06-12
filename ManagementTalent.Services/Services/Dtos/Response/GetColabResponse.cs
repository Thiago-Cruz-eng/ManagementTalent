using System.Runtime;
using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetColabResponse
{
    public string Id { get; set; }
    public string Supervisor { get; set; }
    public string Name { get; set; }
    public DateTime StartAt { get; set; }
    public Guid JobRoleId { get; set; }
    public string SeniorityId { get; set; }
    public string SupervisorId { get; set; }
    public string JobRoleName { get; set; }
    public string SeniorityName { get; set; }
    public string SupervisorName { get; set; }
}
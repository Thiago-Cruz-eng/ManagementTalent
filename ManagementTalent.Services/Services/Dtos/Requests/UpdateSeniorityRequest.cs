using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateSeniorityRequest
{
    public JobRole? JobRoleName { get; set; }
    public int? SeniorityNumber { get; set; }
    public string? SeniorityName { get; set; }

}
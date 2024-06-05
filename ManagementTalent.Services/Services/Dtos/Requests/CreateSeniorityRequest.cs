using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateSeniorityRequest
{
    public string SeniorityName { get; set; }
    public JobRole JobRoleName { get; set; }
    public int SeniorityNumber { get; set; }
}
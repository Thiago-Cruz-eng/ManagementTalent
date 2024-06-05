using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetSeniorityResponse
{
    public JobRole JobRoleName { get; set; }
    public string SeniorityName { get; set; }

    public int SeniorityNumber { get; set; }
}
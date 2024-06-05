using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetSeniorityResponse
{
    public Guid JobRoleId { get; set; }
    public string SeniorityName { get; set; }

    public int SeniorityNumber { get; set; }
}
using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class CreateSeniorityResponse
{
    public string Id { get; set; }
    public JobRole JobRoleName { get; set; }
    public string SeniorityName { get; set; }

    public int SeniorityRelevanceInWorkDay { get; set; }
}
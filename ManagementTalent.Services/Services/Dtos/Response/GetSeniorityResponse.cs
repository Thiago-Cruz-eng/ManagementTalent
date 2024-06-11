using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetSeniorityResponse
{
    public string Id { get; set; }
    public Guid JobRoleId { get; set; }
    public string SeniorityName { get; set; }

    public int SeniorityRelevanceInWorkDay { get; set; }
}
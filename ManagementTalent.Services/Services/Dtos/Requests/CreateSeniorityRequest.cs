using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateSeniorityRequest
{
    public string SeniorityName { get; set; }
    public Guid JobRoleId { get; set; }
    public int SeniorityRelevanceInWorkDay { get; set; }
    public List<string>? SenioritiesIds { get; set; }
}
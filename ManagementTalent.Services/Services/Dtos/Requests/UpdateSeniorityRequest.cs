using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateSeniorityRequest
{
    public Guid? JobRoleId { get; set; }
    public int? SeniorityRelevanceInWorkDay { get; set; }
    public string? SeniorityName { get; set; }
    public List<string>? SenioritiesIds { get; set; }
}
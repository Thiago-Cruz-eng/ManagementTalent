using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateGroupParameterRequest
{
    public string? GroupParamTitle { get; set; }
    public double? Weight { get; set; }
    public Guid AssessmentId { get; set; }
    public List<string>? JobParameterIds { get; set; }
}
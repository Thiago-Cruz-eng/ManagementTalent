using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class CreateGroupParameterResponse
{
    public string Id { get; set; }
    public string GroupParamTitle { get; set; }
    public double Weight { get; set; }
    public Guid AssessmentId { get; set; }
}
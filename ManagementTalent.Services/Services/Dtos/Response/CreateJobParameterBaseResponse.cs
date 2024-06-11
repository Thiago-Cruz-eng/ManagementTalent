using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class CreateJobParameterBaseResponse
{
    public string Id { get; set; }
    public string? JobParamTitle { get; set; }
    public string? Description { get; set; }
    public string? Observation { get; set; }
    public double? Weight { get; set; }
    public double? Expected { get; set; }
    public Seniority? JobSeniorities { get; set; }
}
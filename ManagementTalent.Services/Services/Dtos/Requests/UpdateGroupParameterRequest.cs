using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateGroupParameterRequest
{
    public string? GroupParamTitle { get; set; }
    public double? Weight { get; set; }
    public List<JobParameterBase>? Parameters { get; set; }
}
using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateJobParameterBaseRequest
{
    public string JobParamTitle { get; set; }
    public string Description { get; set; }
    public string Observation { get; set; }
    public double Weight { get; set; }
}
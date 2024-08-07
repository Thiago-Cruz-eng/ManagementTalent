using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateJobParameterBaseRequest
{
    public string JobParamTitle { get; set; }
    public string Description { get; set; }
    public string Observation { get; set; }
    public double Weight { get; set; }
    public int Expected { get; set; }
    public List<string>? GroupParameterIds { get; set; }
    public List<string>? SenioritiesIds { get; set; }
}
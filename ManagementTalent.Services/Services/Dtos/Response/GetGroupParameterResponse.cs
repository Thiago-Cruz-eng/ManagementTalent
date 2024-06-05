using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetGroupParameterResponse
{
    public string GroupParamTitle { get; set; }
    public double Weight { get; set; }
    public List<JobParameterBase> Parameters { get; set; }
}
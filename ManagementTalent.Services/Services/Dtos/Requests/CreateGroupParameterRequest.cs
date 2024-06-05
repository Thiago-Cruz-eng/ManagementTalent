using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateGroupParameterRequest
{
    public string GroupParamTitle { get; set; }
    public double Weight { get; set; }
    public List<JobParameterBase> Parameters { get; set; }
}
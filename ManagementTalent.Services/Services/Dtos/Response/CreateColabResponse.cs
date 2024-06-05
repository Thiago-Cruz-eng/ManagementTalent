using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class CreateColabResponse
{
    public Supervisor Sup { get; set; }
    public string Name { get; set; }
    public DateTime StartAt { get; set; }
    public Seniority Seniority { get; set; }
    public JobRole JobRole { get; set; }
}
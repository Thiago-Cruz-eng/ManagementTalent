using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateColabRequest
{
    public Supervisor Sup { get; set; }
    public string Name { get; set; }
    public DateTime StartAt { get; set; }
    public Seniority Seniority { get; set; }
    public JobRole JobRole { get; set; }
}
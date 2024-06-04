using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class CreateColabResponse
{
    public Supervisor Sup { get; set; }
    public string Name { get; set; }
    public DateTime StartAt { get; set; }
    public string Seniority { get; set; }
    public string JobRole { get; set; }
    public bool Success { get; set; }
}
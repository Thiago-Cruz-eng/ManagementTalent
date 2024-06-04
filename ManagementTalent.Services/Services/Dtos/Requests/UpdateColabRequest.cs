using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateColabRequest
{
    public Supervisor? Sup { get; set; }
    public string? Name { get; set; }
    public DateTime? StartAt { get; set; }
    public string? Seniority { get; set; }
    public string? JobRole { get; set; }
}
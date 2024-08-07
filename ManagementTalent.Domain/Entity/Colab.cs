using ManagementTalent.Domain.Entity.AvaliationContext;
using Microsoft.AspNetCore.Identity;

namespace ManagementTalent.Domain.Entity;

public class Colab : IdentityUser
{
    public string Name { get; set; }
    public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
    public DateTime StartAt { get; set; }
    public Guid JobRoleId { get; set; }
    public string SeniorityId { get; set; }
    public string SupervisorId { get; set; }
    public bool Active { get; set; }
    public string Role { get; set; }
    
    public Seniority Seniority { get; set; }
    public JobRole JobRole { get; set; }
    public Supervisor Supervisor { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateNullAndEmpty(Name);
        ValidateNullAndEmpty(Role);
        ValidateStartAt();

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }

    private void ValidateStartAt()
    {
        if (StartAt > DateTime.Today)
            _validationErrors.Add("SinceAtInJob cannot be in the future.");
    }

    private void ValidateNullAndEmpty(string prop)
    {
        if (string.IsNullOrWhiteSpace(prop))
            _validationErrors.Add("Company is required.");
    }
}
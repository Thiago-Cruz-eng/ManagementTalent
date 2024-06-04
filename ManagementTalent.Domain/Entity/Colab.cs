using Microsoft.AspNetCore.Identity;

namespace ManagementTalent.Domain.Entity;

public class Colab : IdentityUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Supervisor? Sup { get; set; }
    public string Name { get; set; }
    public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
    public DateTime StartAt { get; set; }
    public string Seniority { get; set; }
    public string JobRole { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateNullAndEmpty(Name);
        ValidateNullAndEmpty(Seniority);
        ValidateNullAndEmpty(JobRole);
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
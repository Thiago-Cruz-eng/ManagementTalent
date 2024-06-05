using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Domain.Entity;

public class JobRole
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string JobTitle { get; set; }
    public List<Seniority> Seniorities { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateNullAndEmpty(JobTitle);

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }

    private void ValidateNullAndEmpty(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            _validationErrors.Add("JobTitle is required.");
    }
}
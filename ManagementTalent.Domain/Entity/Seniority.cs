namespace ManagementTalent.Domain.Entity;

public class Seniority
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Dictionary<string, int> SeniorityGrid { get; set; }
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateGroupExist();

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }
    
    private void ValidateNullAndEmpty(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            _validationErrors.Add("description is required.");
    }
    
    private void ValidateGroupExist()
    {
        if (SeniorityGrid.Count <= 0)
            _validationErrors.Add("SeniorityGrid does not have a name.");
    }
}
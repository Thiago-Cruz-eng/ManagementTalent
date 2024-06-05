namespace ManagementTalent.Domain.Entity.AvaliationContext;

public class GroupParameter
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string GroupParamTitle { get; set; }
    public double Weight { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public List<JobParameterBase> Parameters { get; set; }
    
    public List<Assessment> Assessment { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateWeight();
        ValidateNullAndEmpty(GroupParamTitle);

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }
    
    private void ValidateWeight()
    {
        if (Weight <= 0)
            _validationErrors.Add("Result is required.");
    }
    
    private void ValidateNullAndEmpty(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            _validationErrors.Add("description is required.");
    }
}
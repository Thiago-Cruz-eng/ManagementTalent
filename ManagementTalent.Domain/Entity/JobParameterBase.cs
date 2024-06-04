namespace ManagementTalent.Domain.Entity;

public class JobParameterBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string JobParamTitle { get; set; }
    public string Description { get; set; }
    public string Observation { get; set; }
    public double Weight { get; set; }
    public Seniority JobSeniorities { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateWeight();
        ValidateNullAndEmpty(JobParamTitle);
        ValidateNullAndEmpty(Description);
        ValidateGroupExist();

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
    
    private void ValidateGroupExist()
    {
        if (JobSeniorities.SeniorityNumber <= 0)
            _validationErrors.Add("JobSeniorities.SeniorityGrid does not have a name.");
    }
}
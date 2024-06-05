namespace ManagementTalent.Domain.Entity.AvaliationContext;

public class JobParameterBase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string JobParamTitle { get; set; }
    public string Description { get; set; }
    public string Observation { get; set; }
    public double Weight { get; set; }
    public GroupParameter GroupParameter { get; set; }
    public List<Seniority> Seniorities { get; set; }
    public List<JobParameterSeniority> JobParameterSeniorities { get; set; }

    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateWeight();
        ValidateNullAndEmpty(JobParamTitle);
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
        if (Seniorities.Count <= 0)
            _validationErrors.Add("JobSeniorities.SeniorityGrid does not have a name.");
    }
}
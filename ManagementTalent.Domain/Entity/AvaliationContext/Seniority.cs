namespace ManagementTalent.Domain.Entity.AvaliationContext;

public class Seniority
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string SeniorityName { get; set; }
    public int SeniorityNumber { get; set; }
    public List<JobParameterBase> JobParameterBases { get; set; }
    public List<JobParameterSeniority> JobParameterSeniorities { get; set; }

    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateNullAndEmpty();
        ValidateGroupExist();

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }
    
    private void ValidateNullAndEmpty()
    {
        if (string.IsNullOrWhiteSpace(SeniorityName))
            _validationErrors.Add("description is required.");
    }
    
    private void ValidateGroupExist()
    {
        if (SeniorityNumber <= 0)
            _validationErrors.Add("SeniorityName does not have a name.");
    }
}
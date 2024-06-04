namespace ManagementTalent.Domain.Entity;

public class JobRole
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string JobTitle { get; set; }
    public List<GroupParameter> GroupParam { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateNullAndEmpty(JobTitle);
        ValidateGroupExist();

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }

    private void ValidateGroupExist()
    {
        if (GroupParam.Count <= 0)
            _validationErrors.Add("GroupParameters does not have a name.");
    }

    private void ValidateNullAndEmpty(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            _validationErrors.Add("JobTitle is required.");
    }
}
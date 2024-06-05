namespace ManagementTalent.Domain.Entity.AvaliationContext;

public class Assessment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Guid JobRoleId { get; set; }
    public JobRole JobRole { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public ICollection<GroupParameter> GroupParameters { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();
        
        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }

    private void ValidateGroupExist(List<GroupParameter> groupParameters)
    {
        if (GroupParameters.Count <= 0)
            _validationErrors.Add("GroupParameters does not have a name.");
    }

    private void ValidateColab(string collaborator)
    {
        if (string.IsNullOrWhiteSpace(collaborator))
            _validationErrors.Add("collaborator.Name does not have a name.");
    }
}
namespace ManagementTalent.Domain.Entity;

public class Assessment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Colab Collaborator { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime? NextAssessment { get; set; } = DateTime.Now.AddYears(1);
    public List<GroupParameter> GroupParameters { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateColab(Collaborator);
        ValidateGroupExist(GroupParameters);

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }

    private void ValidateGroupExist(List<GroupParameter> groupParameters)
    {
        if (GroupParameters.Count <= 0)
            _validationErrors.Add("GroupParameters does not have a name.");
    }

    private void ValidateColab(Colab collaborator)
    {
        if (string.IsNullOrWhiteSpace(collaborator.Name))
            _validationErrors.Add("collaborator.Name does not have a name.");
    }
}
namespace ManagementTalent.Domain.Entity.AvaliationContext;

public class Seniority
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Guid JobRoleId { get; set; }
    public JobRole JobRoleName { get; set; }
    public string SeniorityName { get; set; }
    public int SeniorityRelevanceInWorkDay { get; set; }
    public List<JobParameterSeniority> JobParameterSeniorities { get; set; }
    
    public void IntegrateSeniority(List<string> ids)
    {
        ids.ForEach(id =>
        {
            JobParameterSeniorities = new List<JobParameterSeniority>
            {
                new()
                {
                    JobParametersBaseId = id,
                    SeniorityId = Id,
                }
            };
        });
    }

    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateGroupExist();

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }
    
    private void ValidateGroupExist()
    {
        if (SeniorityRelevanceInWorkDay <= 0)
            _validationErrors.Add("SeniorityName does not have a name.");
    }
}
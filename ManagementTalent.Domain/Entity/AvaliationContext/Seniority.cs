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
        var list = new List<JobParameterSeniority>();
        ids.ForEach(id =>
        {
            list.Add(new JobParameterSeniority
            {
                JobParametersBaseId = id,
                SeniorityId = Id,
            });
            JobParameterSeniorities = list;
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
namespace ManagementTalent.Domain.Entity.AvaliationContext;

public class JobParameterBase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string JobParamTitle { get; set; }
    public string Description { get; set; }
    public string Observation { get; set; }
    public double Weight { get; set; }
    public int Expected { get; set; }
    public List<GroupParameterJobParameter> GroupParameterJobParameters { get; set; }
    public List<JobParameterSeniority> JobParameterSeniorities { get; set; }
    
    private List<string> _validationErrors;

    public void IntegrateGroupParameter(List<string> ids)
    {
        ids.ForEach(id =>
        {
            GroupParameterJobParameters = new List<GroupParameterJobParameter>
            {
                new()
                {
                    GroupParameterId = id,
                    JobParameterBaseId = Id
                }
            };
        });
    }
    
    public void IntegrateSeniority(List<string> ids)
    {
        ids.ForEach(id =>
        {
            JobParameterSeniorities = new List<JobParameterSeniority>
            {
                new()
                {
                    JobParametersBaseId = Id,
                    SeniorityId = id,
                }
            };
        });
    }
    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateWeight();
        ValidateNullAndEmpty(JobParamTitle);

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
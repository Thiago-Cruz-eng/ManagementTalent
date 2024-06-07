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
        var list = new List<GroupParameterJobParameter>();
        ids.ForEach(id =>
        {
            list.Add(new GroupParameterJobParameter
            {
                GroupParameterId = id,
                JobParameterBaseId = Id
            });
            GroupParameterJobParameters = list;
        });
    }
    
    public void IntegrateSeniority(List<string> ids)
    {
        var list = new List<JobParameterSeniority>();
        ids.ForEach(id =>
        {
            list.Add(new JobParameterSeniority
            {
                JobParametersBaseId = Id,
                SeniorityId = id,
            });
            JobParameterSeniorities = list;
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
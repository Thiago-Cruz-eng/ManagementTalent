namespace ManagementTalent.Domain.Entity;

public class JobParameterSeniority
{
    public Guid JobParameterBaseId { get; set; }
    public JobParameterBase JobParameterBase { get; set; }

    public Guid SeniorityId { get; set; }
    public Seniority Seniority { get; set; }
}
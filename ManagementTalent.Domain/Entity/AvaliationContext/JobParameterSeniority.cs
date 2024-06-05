using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementTalent.Domain.Entity.AvaliationContext;

public class JobParameterSeniority
{
    public string JobParametersBaseId { get; set; } = Guid.NewGuid().ToString();
    public JobParameterBase JobParameterBase { get; set; }

    public Guid SeniorityId { get; set; }
    public Seniority Seniority { get; set; }
}
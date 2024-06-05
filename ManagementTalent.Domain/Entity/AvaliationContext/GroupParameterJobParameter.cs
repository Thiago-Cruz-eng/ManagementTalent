namespace ManagementTalent.Domain.Entity.AvaliationContext;

public class GroupParameterJobParameter
{
    public string GroupParameterId { get; set; } = Guid.NewGuid().ToString();
    public GroupParameter GroupParameter { get; set; }

    public string JobParameterBaseId { get; set; } = Guid.NewGuid().ToString();
    public JobParameterBase JobParameterBase { get; set; } 
}
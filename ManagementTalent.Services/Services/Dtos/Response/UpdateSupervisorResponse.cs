using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class UpdateSupervisorResponse
{
    public Guid? SupFather { get; set; }
    public string Name { get; set; }
    public string Company { get; set; }
    public DateTime? SinceAtInJob { get; set; }
    public int? Age { get; set; }
    public ICollection<Colab>? Colabs { get; set; }
}
using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.AvaliationContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetJobRoleResponse
{
    public string Id { get; set; }
    public string JobTitle { get; set; }
}
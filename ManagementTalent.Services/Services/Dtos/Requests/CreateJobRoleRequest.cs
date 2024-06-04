using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateJobRoleRequest
{
    public string JobTitle { get; set; }
    public List<GroupParameter> GroupParam { get; set; }
}
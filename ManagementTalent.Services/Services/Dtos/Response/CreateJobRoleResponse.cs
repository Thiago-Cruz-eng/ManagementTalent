using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class CreateJobRoleResponse
{
    public string JobTitle { get; set; }
    public List<GroupParameter> GroupParam { get; set; }
}
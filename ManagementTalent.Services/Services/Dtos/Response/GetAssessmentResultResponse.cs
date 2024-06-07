using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetAssessmentResultResponse
{
    public string Id { get; set; }
    public string CollaboratorId { get; set; }
    public string SupervisorId { get; set; }
    public int Result { get; set; }
    public DateTime? NextAssessment { get; set; }

    public List<GroupParam> GroupParams { get; set; }
    public List<JobParam> JobParams { get; set; }
}

public class JobParam
{
    public string Id { get; set; }
    public string JobParamTitle { get; set; }
    public string Description { get; set; }
    public string Observation { get; set; }
    public double Weight { get; set; }
    public int RealityResult { get; set; }
    public string GroupParameterResultId { get; set; }
}

public class GroupParam
{
    public string Id { get; set; }
    public string GroupParamTitle { get; set; }
    public string Weight { get; set; }
    public string AssessmentResultId { get; set; }
}
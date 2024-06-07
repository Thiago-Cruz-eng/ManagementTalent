using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateAssessmentResultRequest
{
    public string CollaboratorId { get; set; }
    public int Result { get; set; }
    public DateTime? NextAssessment { get; set; }

    public List<JobParam> JobParams { get; set; }
}

public class JobParam
{
    public string Id { get; set; }
    public int RealityResult { get; set; }
}
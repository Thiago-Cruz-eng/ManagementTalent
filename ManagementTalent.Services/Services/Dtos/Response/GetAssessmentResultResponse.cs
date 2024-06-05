using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class GetAssessmentResultResponse
{
    public Colab Collaborator { get; set; }
    public List<GroupParameterResult> GroupParameterResults { get; set; }
    public int Result { get; set; }
    public DateTime? NextAssessment { get; set; }
}
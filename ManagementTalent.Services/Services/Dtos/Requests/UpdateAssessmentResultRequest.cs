using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateAssessmentResultRequest
{
    public Colab? Collaborator { get; set; }
    public List<GroupParameterResult>? GroupParameterResults { get; set; }
    public string? SupervisorName { get; set; }
    public int? Result { get; set; }
}
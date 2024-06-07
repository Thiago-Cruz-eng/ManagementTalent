using System.ComponentModel.DataAnnotations;
using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class CreateAssessmentResultResponse
{
    public string Id { get; set; }
    public string CollaboratorId { get; set; }
    public string SupervisorId { get; set; }
    public DateTime? NextAssessment { get; set; }
    public int Result { get; set; }
}
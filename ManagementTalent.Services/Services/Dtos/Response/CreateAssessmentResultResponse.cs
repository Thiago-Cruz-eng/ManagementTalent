using System.ComponentModel.DataAnnotations;
using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;

namespace ManagementTalent.Services.Services.Dtos.Response;

public class CreateAssessmentResultResponse
{
    public Colab Collaborator { get; set; }
    public DateTime? NextAssessment { get; set; }
    public int Result { get; set; }
}
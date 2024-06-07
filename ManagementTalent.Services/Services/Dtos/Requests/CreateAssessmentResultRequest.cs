using System.ComponentModel.DataAnnotations;
using ManagementTalent.Domain.Entity;
using ManagementTalent.Domain.Entity.ResultContext;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateAssessmentResultRequest
{
    public string CollaboratorId { get; set; }
}
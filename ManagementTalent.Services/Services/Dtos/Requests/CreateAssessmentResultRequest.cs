using System.ComponentModel.DataAnnotations;
using ManagementTalent.Domain.Entity;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateAssessmentResultRequest
{
    public Colab Collaborator { get; set; }
    public List<AssessmentParamResult> AssessmentParam { get; set; }
    public string Description { get; set; }
    public string Observation { get; set; }
    public string SupervisorName { get; set; }
    [Range(1, 4, ErrorMessage = "O valor de 'Resultado' deve estar entre 1 e 100.")]
    public int Result { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateAssessmentParamResultRequest
{
    public string? Description { get; set; }
    public string? Observation { get; set; }
    [Range(1, 4, ErrorMessage = "O valor de 'Resultado' deve estar entre 1 e 100.")]
    public int? Result { get; set; }
}
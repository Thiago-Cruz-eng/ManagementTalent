using System.ComponentModel.DataAnnotations;

namespace ManagementTalent.Domain.Entity.ResultContext;

public class AssessmentParamResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string JobParamTitle { get; set; }
    public string Description { get; set; }
    public string Observation { get; set; }
    public double Weight { get; set; }

    [Range(1, 4, ErrorMessage = "O valor de 'Resultado' deve estar entre 1 e 100.")]
    public int RealityResult { get; set; }
    public string GroupParameterResultId { get; set; }
    public GroupParameterResult GroupParameterResult { get; set; }

    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateNullAndEmpty(Description);
        ValidateResult();

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }

    private void ValidateResult()
    {
        if (RealityResult <= 0)
            _validationErrors.Add("Result is required.");
    }

    private void ValidateNullAndEmpty(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            _validationErrors.Add("description is required.");
    }
}
using System.ComponentModel.DataAnnotations;

namespace ManagementTalent.Domain.Entity;

public class AssessmentResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Colab Collaborator { get; set; }
    public List<AssessmentParamResult> AssessmentParam { get; set; }
    public string Description { get; set; }
    public string Observation { get; set; }
    public string SupervisorName { get; set; }

    [Range(1, 4, ErrorMessage = "O valor de 'Resultado' deve estar entre 1 e 100.")]
    public int Result { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateNullAndEmpty(Description);
        ValidateNullAndEmpty(SupervisorName);
        ValidateColab(Collaborator);
        ValidateResult();

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }

    private void ValidateResult()
    {
        if (Result <= 0)
            _validationErrors.Add("Result is required.");
    }

    private void ValidateNullAndEmpty(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            _validationErrors.Add("description is required.");
    }
    
    private void ValidateColab(Colab collaborator)
    {
        if (string.IsNullOrWhiteSpace(collaborator.Name))
            _validationErrors.Add("collaborator.Name does not have a name.");
    }
}
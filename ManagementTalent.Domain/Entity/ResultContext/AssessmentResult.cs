using System.ComponentModel.DataAnnotations;

namespace ManagementTalent.Domain.Entity.ResultContext;

public class AssessmentResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Colab Collaborator { get; set; }
    public DateTime? NextAssessment { get; set; } = DateTime.UtcNow.AddYears(1);
    public List<GroupParameterResult> GroupParameterResults { get; set; }
    public int Result { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();
        
        ValidateColab(Collaborator);

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
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
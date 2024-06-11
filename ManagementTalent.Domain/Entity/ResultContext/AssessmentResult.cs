using System.ComponentModel.DataAnnotations;

namespace ManagementTalent.Domain.Entity.ResultContext;

public class AssessmentResult
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string SupervisorId { get; set; }
    public string CollaboratorId { get; set; }
    public DateTime? NextAssessment { get; set; } = DateTime.UtcNow.AddYears(1);
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public string ActualSeniorityName { get; set; }
    public string ActualJobName { get; set; }
    public string ActualSupervisorName { get; set; }
    public double? Result { get; set; }
    public Colab Collaborator { get; set; }
    public List<GroupParameterResult> GroupParameterResults { get; set; }
    
    private List<string> _validationErrors;

    public void Validate()
    {
        _validationErrors = new List<string>();
        
        ValidateNullAndEmpty(CollaboratorId);

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
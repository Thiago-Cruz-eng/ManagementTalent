namespace ManagementTalent.Domain.Entity;

public class UserSystem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string ColabId { get; set; }
    public bool Active { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    
    private List<string> _validationErrors;

    
    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateNullAndEmpty(Email);
        ValidateNullAndEmpty(Password);
        ValidateNullAndEmpty(Role);

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }

    private void ValidateNullAndEmpty(string prop)
    {
        if (string.IsNullOrWhiteSpace(prop))
            _validationErrors.Add("field is required.");
    }
}
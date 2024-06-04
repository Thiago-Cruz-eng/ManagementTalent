namespace ManagementTalent.Domain.Entity;

public class Supervisor
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Supervisor? SupFather { get; set; }
    public string Name { get; set; }
    public string Company { get; set; }
    public DateTime SinceAtInJob { get; set; }
    public int Age { get; set; }
    public ICollection<Colab> Colabs { get; set; }
    
    private List<string> _validationErrors;
    
    public void Validate()
    {
        _validationErrors = new List<string>();

        ValidateName();
        ValidateAge();
        ValidateCompany();
        ValidateSinceAt();

        if (_validationErrors.Any())
            throw new ArgumentException(string.Join(", ", _validationErrors));
    }

    private void ValidateSinceAt()
    {
        if (SinceAtInJob > DateTime.Today)
            _validationErrors.Add("SinceAtInJob cannot be in the future.");
    }

    private void ValidateCompany()
    {
        if (string.IsNullOrWhiteSpace(Company))
            _validationErrors.Add("Company is required.");
    }

    private void ValidateAge()
    {
        if (Age <= 0)
            _validationErrors.Add("Age must be greater than 0.");
    }

    private void ValidateName()
    {
        if (string.IsNullOrWhiteSpace(Name))
            _validationErrors.Add("Name is required.");
    }
}
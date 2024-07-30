namespace ManagementTalent.Services.Services.Dtos.Requests;

public class CreateUserSystemRequest
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string ColabId { get; set; }
}
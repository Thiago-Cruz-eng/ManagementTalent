namespace ManagementTalent.Services.Services.Dtos.Requests;

public class UpdateUserSystemRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}
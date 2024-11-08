using Microsoft.AspNetCore.Identity;

public class Users : IdentityUser
{
    public string? FullName { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? Expertise { get; set; }
    public int? Experience { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyAddress { get; set; }
}

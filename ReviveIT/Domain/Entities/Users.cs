using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Users : IdentityUser
    {
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

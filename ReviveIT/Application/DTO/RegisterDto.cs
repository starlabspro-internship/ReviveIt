using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }
        public string? Name { get; set; }
        public string? Expertise { get; set; }
        public int? Experience { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
    }
}

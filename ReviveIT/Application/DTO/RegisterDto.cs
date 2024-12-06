using Domain.Entities;
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
        [EnumDataType(typeof(UserRole))]
        public UserRole Role { get; set; }
        public string? Name { get; set; }
        public string? Expertise { get; set; }
        public int? Experience { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }

        [Required(ErrorMessage = "Please select at least one category.")]
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();
    }
}

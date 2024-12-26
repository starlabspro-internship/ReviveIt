using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ChangeEmailDto
    {
        [Required]
        [EmailAddress]
        public string CurrentEmail { get; set; }

        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

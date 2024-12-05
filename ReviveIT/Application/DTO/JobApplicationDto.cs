using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class JobApplicationDto
    {
        public int ApplicationID { get; set; }
        public string ApplicantUserId { get; set; }
        public string Status { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
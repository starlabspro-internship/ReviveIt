using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JobApplication
    {
        public int ApplicationID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; }

        // Foreign keys
        public int JobID { get; set; }
        public Jobs Job { get; set; }

        /*public int UserID { get; set; }
        public User User
        {
            get; set;
        }*/
    }
}

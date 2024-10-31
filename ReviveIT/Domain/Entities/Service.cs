using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Service
    {
        public int ServiceID { get; set; }
        
        public string ServiceName { get; set; }
       
        public string Category { get; set; }
       
        public string Description { get; set; }
        
        public double Price { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        public string UserId { get; set; }
        public Users User { get; set; }
    }
}

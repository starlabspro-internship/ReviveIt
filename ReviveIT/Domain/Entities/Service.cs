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
        [Key]
        public int ServiceID { get; set; }
        [StringLength(50)]
        public string ServiceName { get; set; }
        [StringLength(50)]
        public string Category { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        
        public double Price { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        //Kur krijohet User Tabel
        //public int USerId { get; set; }
        //public User User { get; set; }
    }
}

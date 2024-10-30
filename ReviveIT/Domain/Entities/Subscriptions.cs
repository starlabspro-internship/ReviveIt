using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Subscriptions
    {
        [Key]
        public int SubscriptionId { get; set; } // Primary key
        public string PlanType { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        // public User User { get; set; }
    }
}

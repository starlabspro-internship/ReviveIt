using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Users : IdentityUser
    {
        public string? FullName { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? Expertise { get; set; }
        public int? Experience { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Description { get; set; }
        public bool CompletedProfile { get; set; } = false;

        public ICollection<UserCategory> UserCategories { get; set; }
        public ICollection<OperatingCity> OperatingCities { get; set; }
        public ICollection<PortfolioDocument> Portfolios { get; set; } = new List<PortfolioDocument>();
        public ICollection<ChatSession> TechnicianChatSessions { get; set; }
        public ICollection<ChatSession> CustomerChatSessions { get; set; }
        public ICollection<ChatSession> CompanyChatSessions { get; set; } // Chat sessions where the user is the company

    }

    public enum UserRole
    {
        Admin,
        Customer,
        Technician,
        Company
    }
}

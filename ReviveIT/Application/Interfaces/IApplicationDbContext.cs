using Domain.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<PortfolioDocument> PortfolioDocuments { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<SelectedJobApplicant> SelectedJobApplicants { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<OperatingCity> OperatingCities { get; set; }
        DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<TechnicianAvailability> TechnicianAvailabilities { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
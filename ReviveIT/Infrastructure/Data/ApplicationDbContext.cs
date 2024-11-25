using Application.Interfaces;
using Domain.Configurations;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<UserRefreshToken>UserRefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UsersConfigurations());

            builder.ApplyConfiguration(new JobsConfigurations());

            builder.ApplyConfiguration(new JobsApplicationsConfigurations());

            builder.ApplyConfiguration(new ServicesConfigurations());

            builder.ApplyConfiguration(new SubscriptionsConfiguration());

            builder.ApplyConfiguration(new MessagesConfigurations());

            builder.ApplyConfiguration(new ReviewsConfiguration());

            builder.ApplyConfiguration(new UserRefreshTokenConfigurations());

            builder.ApplyConfiguration(new CategoryConfigurations());

            builder.Entity<Category>().HasData(
              new Category { CategoryID = 1, Name = "Electronics Repair", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
              new Category { CategoryID = 2, Name = "Furniture Restoration", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
              new Category { CategoryID = 3, Name = "Home Appliance Repair", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
              new Category { CategoryID = 4, Name = "Automotive Repair", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );
        }
    }
}

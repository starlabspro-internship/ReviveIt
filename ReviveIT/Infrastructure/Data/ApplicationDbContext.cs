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
        public DbSet<UserCategory> UserCategories { get; set; }
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
            new Category { CategoryID = 4, Name = "Automotive Repair", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 5, Name = "Plumbing Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 6, Name = "Electrical Repairs", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 7, Name = "Cleaning Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 8, Name = "Carpentry", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 9, Name = "Landscaping", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 10, Name = "Painting", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 11, Name = "Roofing", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 12, Name = "HVAC Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 13, Name = "Pest Control", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 14, Name = "Moving Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 15, Name = "Interior Design", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 16, Name = "IT Support", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 17, Name = "Handyman Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 18, Name = "Masonry", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 19, Name = "Welding", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 20, Name = "Security Systems Installation", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 21, Name = "Window Installation", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 22, Name = "Flooring Installation", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 23, Name = "Bathroom Remodeling", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 24, Name = "Kitchen Remodeling", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 25, Name = "Solar Panel Installation", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 26, Name = "Tree Trimming", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 27, Name = "Pool Maintenance", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 28, Name = "Locksmith Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 29, Name = "Event Planning", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 30, Name = "Photography", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 31, Name = "Tutoring", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 32, Name = "Courier Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 33, Name = "Legal Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 34, Name = "Accounting Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 35, Name = "Health and Fitness", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 36, Name = "Child Care", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 37, Name = "Elderly Care", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 38, Name = "Pressure Washing", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 39, Name = "Junk Removal", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 40, Name = "Commercial Cleaning", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 41, Name = "Digital Marketing", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 42, Name = "SEO Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 43, Name = "Social Media Management", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 44, Name = "Web Development", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 45, Name = "Graphic Design", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 46, Name = "Content Writing", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 47, Name = "Video Editing", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 48, Name = "3D Printing", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 49, Name = "Custom Software Development", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 50, Name = "Mobile App Development", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 51, Name = "Photography Editing", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 52, Name = "Data Entry Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 53, Name = "Virtual Assistance", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 54, Name = "Business Consulting", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 55, Name = "Market Research", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 56, Name = "Project Management", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 57, Name = "Branding", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 58, Name = "Event Coordination", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 59, Name = "Public Relations", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 60, Name = "Translation Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 61, Name = "Voiceover Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 62, Name = "Legal Consultation", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 63, Name = "Property Management", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 64, Name = "Real Estate Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 65, Name = "Insurance Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 66, Name = "Financial Planning", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 67, Name = "Investment Advice", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 68, Name = "Tax Preparation", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 69, Name = "Debt Counseling", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 70, Name = "Retirement Planning", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 71, Name = "Mortgage Advice", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 72, Name = "Estate Planning", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 73, Name = "Human Resources", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 74, Name = "Recruitment Services", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { CategoryID = 75, Name = "Employee Training", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );
        }
    }
}

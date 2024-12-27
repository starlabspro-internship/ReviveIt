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
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<PortfolioDocument> PortfolioDocuments { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; } 
        public DbSet<SelectedJobApplicant> SelectedJobApplicants { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<OperatingCity> OperatingCities { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new FeedbackConfiguration());
            builder.ApplyConfiguration(new UsersConfigurations());
            builder.ApplyConfiguration(new JobsConfigurations());
            builder.ApplyConfiguration(new JobsApplicationsConfigurations());
            builder.ApplyConfiguration(new ServicesConfigurations());
            builder.ApplyConfiguration(new SubscriptionsConfiguration());
            builder.ApplyConfiguration(new MessagesConfigurations());
            builder.ApplyConfiguration(new ReviewsConfiguration());
            builder.ApplyConfiguration(new UserRefreshTokenConfigurations());
            builder.ApplyConfiguration(new CategoryConfigurations());
            builder.ApplyConfiguration(new PortfolioDocumentConfiguration());
            builder.ApplyConfiguration(new ChatSessionConfigurations());
            builder.ApplyConfiguration(new SelectedJobApplicantConfigurations());
            builder.ApplyConfiguration(new UserCategoryConfigurations());
            builder.ApplyConfiguration(new CityConfigurations());
            builder.ApplyConfiguration(new OperatingCityConfigurations());
          
            builder.Entity<City>().HasData(
                new City { CityId = 1, CityName = "Deçan" },
                new City { CityId = 2, CityName = "Dragash" },
                new City { CityId = 3, CityName = "Drenas" },
                new City { CityId = 4, CityName = "Ferizaj" },
                new City { CityId = 5, CityName = "Fushë Kosovë" },
                new City { CityId = 6, CityName = "Gjakovë" },
                new City { CityId = 7, CityName = "Gjilan" },
                new City { CityId = 8, CityName = "Istog" },
                new City { CityId = 9, CityName = "Kaçanik" },
                new City { CityId = 10, CityName = "Kamenicë" },
                new City { CityId = 11, CityName = "Klinë" },
                new City { CityId = 12, CityName = "Lipjan" },
                new City { CityId = 13, CityName = "Malishevë" },
                new City { CityId = 14, CityName = "Mitrovicë" },
                new City { CityId = 15, CityName = "Obiliq" },
                new City { CityId = 16, CityName = "Pejë" },
                new City { CityId = 17, CityName = "Podujevë" },
                new City { CityId = 18, CityName = "Prishtinë" },
                new City { CityId = 19, CityName = "Prizren" },
                new City { CityId = 20, CityName = "Rahovec" },
                new City { CityId = 21, CityName = "Skenderaj" },
                new City { CityId = 22, CityName = "Suharekë" },
                new City { CityId = 23, CityName = "Shtërpcë" },
                new City { CityId = 24, CityName = "Shtime" },
                new City { CityId = 25, CityName = "Viti" },
                new City { CityId = 26, CityName = "Vushtrri" }
            );
        }
    }
}

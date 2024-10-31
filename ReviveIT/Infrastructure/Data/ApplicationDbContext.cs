using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Configurations;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Messages> Messages { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UsersConfigurations());
            builder.ApplyConfiguration(new JobsConfigurations());
            builder.ApplyConfiguration(new JobsApplicationsConfigurations());
            builder.ApplyConfiguration(new ServicesConfigurations());
            builder.ApplyConfiguration(new SubscriptionsConfiguration());
            builder.ApplyConfiguration(new MessagesConfigurations());
        }

    }
}

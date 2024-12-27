using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations
{
    public class TechnicianAvailabilityConfigurations : IEntityTypeConfiguration<TechnicianAvailability>
    {
        public void Configure(EntityTypeBuilder<TechnicianAvailability> builder)
        {
            builder.HasKey(c => c.AvailabilityID);

            builder.Property(e => e.DaysAvailable).IsRequired().HasMaxLength(1000);
            builder.Property(e => e.MonthsUnavailable).IsRequired().HasMaxLength(1000);
            builder.Property(e => e.SpecificUnavailableDates).IsRequired().HasMaxLength(1000);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(e => e.Technician)
                   .WithMany()
                   .HasForeignKey(e => e.TechnicianId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

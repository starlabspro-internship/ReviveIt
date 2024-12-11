using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public class OperatingCityConfigurations : IEntityTypeConfiguration<OperatingCity>
    {
        public void Configure(EntityTypeBuilder<OperatingCity> builder) 
        {
            builder.HasKey(oc => oc.OperatingCityId);

            builder.HasOne(oc => oc.User)
                   .WithMany(u => u.OperatingCities)
                   .HasForeignKey(oc => oc.userId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oc => oc.City)
                   .WithMany(c => c.OperatingCities)
                   .HasForeignKey(tc => tc.CityId);
        }
    }
}

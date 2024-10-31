using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;



namespace Domain.Configurations
{
    public class UsersConfigurations : IEntityTypeConfiguration<Users>
    {

        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(u => u.Id);
           // builder.Property(u => u.UserId).ValueGeneratedOnAdd();
            builder.Property(u => u.Role).IsRequired().HasMaxLength(50);
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(u => u.UpdatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}

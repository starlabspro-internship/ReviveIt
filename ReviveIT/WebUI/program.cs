﻿using Application.Features.Accounts;
using Application.Features.User;
using Application.Helpers;
using Application.Interfaces;
using Domain.Constants;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebUI.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infrastructure")));

builder.Services.AddIdentity<Users, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy => policy.RequireAuthenticatedUser());
});

builder.Services.AddScoped<TokenHelper>();
builder.Services.AddScoped<LoginFeature>();
builder.Services.AddScoped<RegisterFeature>();
builder.Services.AddScoped<RefreshTokenRepository>();
builder.Services.AddScoped<ProfilePictureFeature>();
builder.Services.AddScoped<UpdateProfileFeature>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration[ConfigurationConstant.Issuer],
        ValidAudience = builder.Configuration[ConfigurationConstant.Audience],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[ConfigurationConstant.Key]))
    };
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IJobsRepository, JobsRepository>();
builder.Services.AddScoped<IJobApplicationsRepository, JobApplicationsRepository>();
builder.Services.AddScoped<IServicesRepository, ServicesRepository>();
builder.Services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IMessagesRepository, MessagesRepository>();
builder.Services.AddScoped<IReviewsRepository, ReviewsRepository>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ConfigurationConstant>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await AddUserRoles.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "company",
    pattern: "Company/{action=Index}/{id?}",
    defaults: new { controller = "Company", action = "Index" });

app.MapControllerRoute(
    name: "companyInbox",
    pattern: "Company/Inbox/{id?}",
    defaults: new { controller = "Company", action = "Inbox" });

app.MapControllerRoute(
    name: "companyMyAccount",
    pattern: "Company/MyAccount/{id?}",
    defaults: new { controller = "Company", action = "MyAccount" });

app.MapControllerRoute(
    name: "technician",
    pattern: "Technician/{action=Index}/{id?}",
    defaults: new { controller = "Technician" });

app.MapControllerRoute(
    name: "technicianPostedJobs",
    pattern: "Technician/PostedJobs/{id?}",
    defaults: new { controller = "Technician", action = "PostedJobs" });

app.MapControllerRoute(
    name: "technicianMyAccount",
    pattern: "Technician/Myaccount/{id?}",
    defaults: new { controller = "Technician", action = "Myaccount" });

app.MapControllerRoute(
    name: "customer",
    pattern: "Customer/{action=Inbox}/{id?}",
    defaults: new { controller = "Customer" });

app.MapControllerRoute(
    name: "customerTechniciansCompanies",
    pattern: "Customer/TechniciansCompanies/{id?}",
    defaults: new { controller = "Customer", action = "TechniciansCompanies" });

app.MapControllerRoute(
    name: "customerPostJob",
    pattern: "Customer/PostJob/{id?}",
    defaults: new { controller = "Customer", action = "PostJob" });

app.MapControllerRoute(
    name: "customerMyAccount",
    pattern: "Customer/MyAccount/{id?}",
    defaults: new { controller = "Customer", action = "MyAccount" });

app.Run();
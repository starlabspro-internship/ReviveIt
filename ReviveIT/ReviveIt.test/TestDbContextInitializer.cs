using Infrastructure.Data;

namespace ReviveIt.test.Providers;

public static class TestDbContextInitializer
{
    public static void SeedTestData(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        context.Users.Add(new Users
        {
            Email = "testuser@example.com",
            Role = UserRole.Admin 
        });

        context.SaveChanges();
    }
}

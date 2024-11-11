using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using ReviveIt.test.Providers;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTestServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));

        services.AddSingleton<IHttpContextAccessor, MockHttpContextAccessor>();

        return services;
    }
}

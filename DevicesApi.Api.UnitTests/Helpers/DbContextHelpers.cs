using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevicesApi.Api.UnitTests.Helpers;

public static class DbContextHelpers
{
    public static IServiceCollection RemoveDbContext(this IServiceCollection serviceCollection)
    {
        var descriptor = serviceCollection.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DeviceDbContext>));
        if (descriptor != null)
        {
            serviceCollection.Remove(descriptor);
        }

        return serviceCollection;
    }

    public static IServiceCollection AddInMemorySql(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddDbContext<DeviceDbContext>(
            options => { options.UseInMemoryDatabase(Guid.NewGuid().ToString()); },
            ServiceLifetime.Singleton,
            ServiceLifetime.Singleton);
    }
}
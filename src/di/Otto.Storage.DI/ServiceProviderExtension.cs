using Otto.Storage.City;
using Otto.Storage.City.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace Otto.Storage.DI;

public static class ServiceProviderExtension
{
    public static async Task ExecuteSeedCityIfNeed(this IServiceProvider provider, CancellationToken token)
    {
        using var scope = provider.CreateScope();
        await scope.ServiceProvider.GetRequiredService<IInitializationService<CityDbContext>>()
            .ExecuteAsync(token);
    }
}
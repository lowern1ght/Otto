using Otto.Storage.City;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Otto.Storage.City.Seed;

namespace Otto.Storage.DI;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCityDbContext(this IServiceCollection collection, 
        Action<DbContextOptionsBuilder> action)
    {
        return collection.AddDbContext<CityDbContext>(action);
    }

    public static IServiceCollection AddCitySeedInitialization(this IServiceCollection collection)
    {
        return collection.AddScoped<IInitializationService<CityDbContext>, CityInitializationService>();
    }
}
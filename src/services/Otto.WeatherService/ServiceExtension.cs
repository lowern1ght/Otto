using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenMeteo;

namespace Otto.WeatherService;

public static class ServiceExtension
{
    public static IServiceCollection AddWeatherService(this IServiceCollection collection)
    {
        collection.TryAddTransient<IWeatherService<WeatherForecast>, WeatherClient>();
        return collection;
    }
}
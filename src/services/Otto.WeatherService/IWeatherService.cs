using OpenMeteo;

namespace Otto.WeatherService;

public interface IWeatherService<TResult>
{
    Task<TResult> GetWeatherAsync(string cityName, CancellationToken token);
    Task<TResult> GetWeatherAsync(float lat, float lon, CancellationToken token);
}
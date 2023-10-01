using OpenMeteo;

namespace Otto.WeatherService;

public class WeatherClient : IWeatherService<WeatherForecast>
{
    private readonly OpenMeteoClient _openMeteoClient = new();
    
    /// <summary>
    /// Execute request to open-meteo api per city
    /// </summary>
    /// <param name="cityName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="InvalidWeatherException"></exception>
    public async Task<WeatherForecast> GetWeatherAsync(string cityName, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        return await _openMeteoClient.QueryAsync(cityName)
            ?? throw new InvalidWeatherException(cityName);
    }

    /// <summary>
    /// Execute request to open-meteo api per coordinates 
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="InvalidWeatherException"></exception>
    public async Task<WeatherForecast> GetWeatherAsync(float lat, float lon, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        return await _openMeteoClient.QueryAsync(lat, lon)
               ?? throw new InvalidWeatherException(lat, lon);
    }
}
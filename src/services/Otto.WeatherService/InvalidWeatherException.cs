namespace Otto.WeatherService;

public class InvalidWeatherException : Exception
{
    public InvalidWeatherException(string city)
        : base($"Response for city '{city}' is empty") { }
    
    public InvalidWeatherException(float lat, float log)
        : base($"Response for coordinates | {lat} | {log} | is empty") { }
}
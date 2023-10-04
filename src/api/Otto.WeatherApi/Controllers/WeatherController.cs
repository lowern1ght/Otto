using AutoMapper;
using OpenMeteo;
using Otto.WeatherService;
using Otto.WeatherApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Otto.WeatherApi.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : Controller
{
    private readonly IMapper _mapper;
    private readonly ILogger<WeatherController> _logger;
    private readonly IWeatherService<WeatherForecast> _weatherService;
    
    public WeatherController(ILogger<WeatherController> logger,
        IWeatherService<WeatherForecast> weatherService, 
        IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _weatherService = weatherService;
    }
    
    [HttpGet("city")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ResponseCache(Duration = 1200, Location = ResponseCacheLocation.Client)]
    public async Task<ActionResult> WeatherCityAsync(
        [FromQuery] string name, 
        CancellationToken token)
    {
        return await TryGetWeatherAsync(_weatherService.GetWeatherAsync(name, token));
    }

    [HttpGet("coord")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ResponseCache(Duration = 1200, Location = ResponseCacheLocation.Client)]
    public async Task<ActionResult> WeatherCoordinateAsync(
        [FromQuery] float latitude, 
        [FromQuery] float longitude,
        CancellationToken token)
    {
        return await TryGetWeatherAsync(_weatherService.GetWeatherAsync(latitude, longitude, token));
    }

    private async Task<ActionResult> TryGetWeatherAsync(Task<WeatherForecast> action)
    {
        try
        {
            var model = _mapper.Map<WeatherModel>(await action);
            return Ok(model);
        }
        catch(Exception exception)
            when(exception is InvalidWeatherException or OperationCanceledException)
        {
            _logger.Log(LogLevel.Error, exception.Message);
            return BadRequest(exception.Message);
        }
    }
}
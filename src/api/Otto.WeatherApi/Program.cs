using OpenMeteo;
using Otto.WeatherApi.Models;
using Otto.WeatherService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer()
        .AddSwaggerGen();
}

builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<WeatherForecast, WeatherModel>()
        .ForMember(model => model.CurrentTemperature, forecast =>
        {
            forecast.MapFrom<float>(weatherForecast => weatherForecast.CurrentWeather!.Temperature);
        })
        .ReverseMap();
});

builder.Services.AddWeatherService();

var application = builder.Build();

if (application.Environment.IsDevelopment())
{
    application.UseSwagger();
    application.UseSwaggerUI();
}

application.UseHttpsRedirection();

application.UseAuthorization();

application.MapControllers();

application.Run();
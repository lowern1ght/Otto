using System.Globalization;
using Serilog.Events;
using Otto.Logger.DI;
using Otto.Storage.DI;
using Otto.Storage.City;
using Microsoft.EntityFrameworkCore;
using Otto.CityApi.Models;
using Otto.Storage.City.Entity;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLoggingOtto(builder.Environment.IsDevelopment() 
    ? LogEventLevel.Debug
    : LogEventLevel.Information);

builder.Services.AddRouting(options => options.LowercaseUrls = true)
    .AddControllers();

var parameters = builder.Configuration
    .GetSettingsToDbContext<CityDbContext>();

var connectionBuilder = builder.Configuration
    .GetConnectionStringFromSettings(parameters);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer()
        .AddSwaggerGen();
}

builder.Services.AddAutoMapper(expression =>
{
    expression.CreateMap<CityModel, City>()
        .ReverseMap();
});

builder.Services.AddCityDbContext(optionsBuilder
    => optionsBuilder.UseSnakeCaseNamingConvention(CultureInfo.InvariantCulture)
        .UseNpgsql(connectionBuilder.ConnectionString));

builder.Services.AddCitySeedInitialization();

var application = builder.Build();

if (application.Environment.IsDevelopment())
{
    application.UseSwagger()
        .UseSwaggerUI();
}

await application.Services
    .ExecuteSeedCityIfNeed(new CancellationToken());

application.MapGet("/", () 
    => $"{typeof(Program).Namespace} is ready");

application.MapDefaultControllerRoute();

application.Run();
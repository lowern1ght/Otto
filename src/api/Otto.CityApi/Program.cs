using System.Globalization;
using System.Reflection;
using Serilog.Events;
using Otto.Logger.DI;
using Otto.Storage.DI;
using Otto.Storage.City;
using Otto.CityApi.Models;
using Otto.Storage.City.Entity;
using Microsoft.EntityFrameworkCore;

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

application.UseHttpsRedirection();

application.MapGet("/", () 
    => $"{Assembly.GetExecutingAssembly().FullName} is ready");

application.MapDefaultControllerRoute();

application.Run();
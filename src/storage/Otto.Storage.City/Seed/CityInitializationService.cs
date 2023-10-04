using System.Globalization;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Otto.Storage.City.Entity;
using Otto.Storage.City.Seed.Models;

namespace Otto.Storage.City.Seed;

public class CityInitializationService : IInitializationService<CityDbContext>
{
    private readonly CityDbContext _dbContext;
    private readonly ILogger<CityInitializationService> _logger;

    public CityInitializationService(ILogger<CityInitializationService> logger, CityDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    private const string DefaultFileName = "worldcities.csv";
    private const string DefaultResourcesFolderName = "Resources";
    
    public async Task ExecuteAsync(CancellationToken token)
    {
        try
        {
            await _dbContext.Database.MigrateAsync(token);
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Critical, exception.Message);
            return;
        }
     
        if (_dbContext.Cities.Any())
        {
            _logger.Log(LogLevel.Information, "Initialization not started, cities count is not 0");
            return;
        }
        
        var path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, 
            DefaultResourcesFolderName, 
            DefaultFileName);
        
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        var csvModels = csv.GetRecords<CityCsvModel>().ToArray()
            ?? throw new InvalidOperationException("Error parse csv to models");

        _logger.Log(LogLevel.Information, "Start initialization count {RowCount}", 
            csvModels.Length);
        
        var listCountry = csvModels
            .Select(model => model.Country)
            .Distinct()
            .Select(title => new Country { Title = title })
            .ToList();

        var listCities = csvModels
            .Select(model 
                =>
            {
                return new Entity.City
                {
                    Title = model.Name,
                    Country = listCountry.First(country => country.Title.Equals(model.Country)),
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                };
            })
            .ToArray();

        var transaction = await _dbContext.Database.BeginTransactionAsync(token);

        try
        {
            await _dbContext.AddRangeAsync(listCities, token);
            await _dbContext.SaveChangesAsync(token);
            await transaction.CommitAsync(token);
        }
        catch (Exception exception)
        {
            _logger.Log(LogLevel.Error, exception.Message);
            await transaction.RollbackAsync(token);
        }
    }
}
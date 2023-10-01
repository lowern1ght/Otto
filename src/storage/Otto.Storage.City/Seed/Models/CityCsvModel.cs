using CsvHelper.Configuration.Attributes;

namespace Otto.Storage.City.Seed.Models;

public class CityCsvModel
{
    [Index(0)]
    [Name("city")]
    public string Name { get; set; }
    
    [Index(2)]
    [Name("lat")]
    public float Latitude { get; set; }
    
    [Index(3)]
    [Name("lng")]
    public float Longitude { get; set; }
    
    [Index(4)]
    [Name("country")]
    public string Country { get; set; }
}
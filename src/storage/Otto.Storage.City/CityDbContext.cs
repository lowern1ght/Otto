using Otto.Storage.City.Entity;
using Microsoft.EntityFrameworkCore;

namespace Otto.Storage.City;

public class CityDbContext : DbContext
{
    public CityDbContext(DbContextOptions<CityDbContext> options)
        : base(options) { }
    
    public DbSet<Entity.City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
}
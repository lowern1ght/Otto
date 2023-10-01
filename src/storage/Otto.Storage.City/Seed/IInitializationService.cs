using Microsoft.EntityFrameworkCore;

namespace Otto.Storage.City.Seed;

public interface IInitializationService<TDbContext>
    where TDbContext : DbContext
{
    public Task ExecuteAsync(CancellationToken token);
}
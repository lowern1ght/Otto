using Microsoft.EntityFrameworkCore;

namespace Otto.Storage.DI;

public class DbContextParameters<TDbContext> 
    where TDbContext : DbContext
{
    public string? Host { get; set; }
    public int?    Port { get; set; }
    public string? Database { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
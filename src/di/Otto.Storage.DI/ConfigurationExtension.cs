using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Otto.Storage.DI;

public static class ConfigurationExtension
{
    public static DbContextParameters<TDbContext> GetSettingsToDbContext<TDbContext>(this IConfiguration configuration)
        where TDbContext : DbContext
    {
        var section = configuration.GetSection(typeof(TDbContext).Name ?? 
                                               throw new ArgumentNullException(nameof(TDbContext)));

        return section.Get<DbContextParameters<TDbContext>>()
            ?? throw new InvalidCastException($"Fail cast section: {section} to type: {typeof(TDbContext)}");
    }

    public static NpgsqlConnectionStringBuilder GetConnectionStringFromSettings<TDbContext>(this IConfiguration configuration,
        DbContextParameters<TDbContext> parameters) where TDbContext : DbContext
    {
        var mapper = new MapperConfiguration(expression =>
        {
            expression.CreateMap<NpgsqlConnectionStringBuilder, DbContextParameters<TDbContext>>()
                .ReverseMap();
        }).CreateMapper();

        return mapper.Map<NpgsqlConnectionStringBuilder>(parameters);
    }
}
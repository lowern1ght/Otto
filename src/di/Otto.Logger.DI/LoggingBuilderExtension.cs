using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using ILogger = Serilog.ILogger;

namespace Otto.Logger.DI;

public static class LoggingBuilderExtension
{
    public static ILoggingBuilder AddLoggingOtto(this ILoggingBuilder builder,
        LogEventLevel level = LogEventLevel.Information)
    {
        return builder.ClearProviders()
            .AddSerilog(DefaultLogger(level));
    }

    private static ILogger DefaultLogger(LogEventLevel level)
    {
        return new LoggerConfiguration()
            .MinimumLevel.Is(level)
            .WriteTo.Console(theme: SystemConsoleTheme.Colored)
            .CreateLogger();
    }
}
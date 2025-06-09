using CodeMechanic.Shargs;
using Serilog;
using Serilog.Core;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace mavlink;

internal class Program
{
    static async Task Main(string[] args)
    {
        var arguments = new ArgsMap(args);

        var logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(
                ".mavlink/logs/mavlink.log",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true
            )
            .CreateLogger();

        bool run_as_cli = !arguments.HasCommand("web");

        if (run_as_cli) await RunAsCli(arguments, logger);
    }

    static async Task RunAsCli(ArgsMap arguments, Logger logger)
    {
        RunFiglet();
        var services = CreateServices(arguments, logger);
        Application app = services.GetRequiredService<Application>();
        await app.Run();
    }

    private static void RunFiglet()
    {
        // var font = FigletFont.Load("starwars.flf");

        AnsiConsole.Write(
            new FigletText(nameof(mavlink))
                .LeftJustified()
                .Color(Color.Red));
    }

    private static ServiceProvider CreateServices(
        ArgsMap arguments,
        Logger logger)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton(arguments)
            .AddSingleton<Logger>(logger)
            .AddSingleton<Application>()
            .AddSingleton<FuzzySearchService>()
            .AddSingleton<WeatherService>()
            .BuildServiceProvider();

        return serviceProvider;
    }
}
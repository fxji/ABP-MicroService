using System;
using System.Threading.Tasks;
using FeedbackWorkerService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Volo.Abp;

namespace FeedbackWorkerService;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        // 启动前日志（硬编码）——用于捕捉初始化错误
        var logPath = Path.Combine(AppContext.BaseDirectory, "Logs", "logs.txt");
        Directory.CreateDirectory(Path.GetDirectoryName(logPath)!);

        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.Console())
            .WriteTo.Async(c => c.File(logPath, rollingInterval: RollingInterval.Day))
            .CreateLogger();

        try
        {
            Log.Information("Starting console host.");

            // Create a HostBuilder instance using the default settings.
            // This will configure the host to use the console as the lifetime
            // and to use Autofac as the IoC container.
            var builder = Host.CreateDefaultBuilder(args)
                .UseConsoleLifetime()
                .UseWindowsService()
                .UseAutofac();

            // Configure Serilog as the logging provider.
            // Read the Serilog settings from the configuration file
            // and from the services registered in the container.
            // Also, enrich the log messages with the log context.
            builder.UseSerilog(
                (context, services, config) =>
                {
                    config
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext();
                }
                );

            // Configure the services for the host.
            // Register the Worker class as a hosted service.
            // Also, register the FeedbackWorkerServiceModule as an application module.
            // Replace the configuration of the module with the configuration
            // of the host.
            builder.ConfigureServices((context, services) =>
            {
                services.AddHostedService<Worker>();
                services.AddApplicationAsync<FeedbackWorkerServiceModule>(options =>
                {
                    options.Services.ReplaceConfiguration(context.Configuration);
                });
            });

            var host = builder.Build();

            await host.Services
                .GetRequiredService<IAbpApplicationWithExternalServiceProvider>()
                .InitializeAsync(host.Services);

            await host.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

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
        // Configure Serilog logging
        //
        // MinimumLevel is set to Debug if we're in debug mode, otherwise it's set to Information.
        // This means that when we're running in debug mode, we'll get more detailed logs, but in
        // production mode, we'll only log important information.
        //
        // We're also overriding the minimum level for logs from Microsoft. This is because some
        // Microsoft libraries are very chatty and we don't want to see all of their debug logs.
        // Instead, we're setting the minimum level for Microsoft logs to Information, which means
        // we'll only see their information logs and not their debug logs.
        //
        // The Enrich.FromLogContext() call is used to enrich the log with properties from the
        // current log context. This means that we'll get properties like the current request ID,
        // the current user, etc. in our logs.
        //
        // The WriteTo.Async(c => c.File("Logs/logs.txt")) call is used to write the logs to a
        // file. The "Logs/logs.txt" file will be created in the root of the application and will
        // contain all of the logs.
        //
        // The WriteTo.Async(c => c.Console()) call is used to write the logs to the console.
        // This is useful when we're running the application in a console or terminal and want to
        // see the logs in real time.
        //
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting console host.");

            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(services =>
             {
                 services.AddHostedService<Worker>();
                 services.AddApplicationAsync<FeedbackWorkerServiceModule>(options =>
                 {
                     options.Services.ReplaceConfiguration(services.GetConfiguration());
                     options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
                 });
             }).UseAutofac();

            // In development environment, run as a console application, in production environment, run as a Windows service
            if (Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development")
            {
                builder.UseConsoleLifetime();
            }
            else
            {
                builder.UseWindowsService();
            }

            var host = builder.Build();
            await host.Services.GetRequiredService<IAbpApplicationWithExternalServiceProvider>().InitializeAsync(host.Services);

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
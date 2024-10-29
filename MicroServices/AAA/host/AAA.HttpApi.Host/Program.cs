using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace AAA;

//public class Program
//{

//    public async static Task<int> Main(string[] args)
//    {
//        Log.Logger = new LoggerConfiguration()
//#if DEBUG
//            .MinimumLevel.Debug()
//#else
//            .MinimumLevel.Information()
//#endif
//            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
//            .Enrich.FromLogContext()
//            .WriteTo.Async(c => c.File("Logs/logs.txt"))
//            .WriteTo.Async(c => c.Console())
//            .CreateLogger();

//        try
//        {
//            Log.Information("Starting web host.");
//            var builder = WebApplication.CreateBuilder(args);
//            builder.Host.AddAppSettingsSecretsJson()
//                .UseAutofac()
//                .UseSerilog();
//            await builder.AddApplicationAsync<AAAHttpApiHostModule>();
//            var app = builder.Build();
//            await app.InitializeApplicationAsync();
//            await app.RunAsync();
//            return 0;
//        }
//        catch (Exception ex)
//        {
//            Log.Fatal(ex, "Host terminated unexpectedly!");
//            return 1;
//        }
//        finally
//        {
//            Log.CloseAndFlush();
//        }
//    }
//}


public class Program
{

    public async static Task<int> Main(string[] args)
    {

        //全局日志记录器
        Log.Logger = new LoggerConfiguration()

#if DEBUG
            //调试情况下 最低记录登记
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            //日志调用类命名空间如果以 Microsoft 开头，覆盖日志输出最小级别为 Information
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)

            //日志调用类命名空间如果以 Microsoft.EntityFrameworkCore 开头，覆盖日志输出最小级别为 Warning
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)

            .Enrich.FromLogContext()

            .WriteTo.Async(c => c.File("Logs/logs.txt",
                outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {SourceContext} {NewLine}{Message}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true))
#if DEBUG
            //配置日志输出到控制台
            .WriteTo.Async(c => c.Console())
#endif
            .CreateLogger();

        try
        {

            Log.Information("Starting Web.HttpApi.Host.");

            //创建应用
            var builder = WebApplication.CreateBuilder(args);
            builder.Host
#if DEBUG
                //Secrets文件添加
                .AddAppSettingsSecretsJson()
#endif
                //使用Autofac作为默认的IOC
                .UseAutofac()
                //使用 Serilog作为默认的日志记录器
                .UseSerilog();

            //添加模块
            await builder.AddApplicationAsync<AAAHttpApiHostModule>();
            var app = builder.Build();

            //运行
            await app.InitializeApplicationAsync();
            await app.RunAsync();
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

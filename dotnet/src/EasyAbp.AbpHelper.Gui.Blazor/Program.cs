using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace EasyAbp.AbpHelper.Gui.Blazor
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File(
                    "Logs/logs.txt",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 60))
                .WriteTo.Async(c => c.File(
                    "Logs/Errors/errors.txt",
                    restrictedToMinimumLevel: LogEventLevel.Error,
                    rollingInterval: RollingInterval.Minute,
                    fileSizeLimitBytes: 10 * 1024 * 1024,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 10))
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
                .CreateLogger();

            try
            {
                Log.Information("Starting web host.");
                CreateHostBuilder(args).Build().Run();
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();
    }
}

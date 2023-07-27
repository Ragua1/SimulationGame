using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SimulationGame.Service;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        Console.WriteLine("Starting up");

        try
        {
            await host.RunAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            // NLog: catch any exception and log it.
            Console.WriteLine(ex);
            throw;
        }
        finally
        {
            Console.WriteLine("Stopped");

            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            //ApplicationLogging.FlushProviders();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            //.ConfigureLogging(logging =>
            //{
            //    logging.ClearProviders();
            //    logging.AddConsole();
            //    logging.AddDebug();
            //})
            .ConfigureAppConfiguration((context, config) =>
            {
                // Configure the app here.
                config.SetBasePath(AppContext.BaseDirectory);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<WindowsBackgroundService>();
                //services.AddApplicationLayer();
            });
}
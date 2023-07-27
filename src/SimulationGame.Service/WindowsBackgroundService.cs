using Microsoft.Extensions.Hosting;
using SimulationGame.Logic;

namespace SimulationGame.Service;

public class WindowsBackgroundService : BackgroundService
{
    private Thread? restApiThread;
    private Thread? gameThread;
    private string RestApiUri => "http://localhost:5000";

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var game = new GameLogic();
        
        API.Controllers.SettlementsController.GameLogic = game;
        API.Controllers.RoutesController.GameLogic = game;
        API.Controllers.VehiclesController.GameLogic = game;
        
        gameThread = new Thread(async _ =>
        {
            game.NewGame();
        })
        {
            Name = "Game thread"
        };
        restApiThread = new Thread(async _ =>
        {
            // init REST api
            try
            {
                var builder = API.Program.CreateWebAppBuilder(new[] { "%LAUNCHER_ARGS%" });

                builder.Host.UseWindowsService();
                var app = builder.Build();

                app.Urls.Add(RestApiUri);
                Console.WriteLine($"Run up WebApi on url '{string.Join(", ", app.Urls)}'.");

                API.Program.ConfigureApp(app);

                await app.RunAsync(stoppingToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("WebApi is down.");
            }
        })
        {
            Name = "REST api thread"
        };

        Console.WriteLine($"Start thread '{gameThread.Name}'");
        gameThread.Start();
        Console.WriteLine($"Start thread '{restApiThread.Name}'");
        restApiThread.Start();

        Console.WriteLine($"Thread '{gameThread.Name}' state: '{gameThread.ThreadState}'");
        Console.WriteLine($"Thread '{restApiThread.Name}' state: '{restApiThread.ThreadState}'");
        
        return Task.CompletedTask;
    }
}
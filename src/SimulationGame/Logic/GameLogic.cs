using System.Text.Json;
using SimulationGame.Models;
using SimulationGame.UI;

namespace SimulationGame.Logic;

internal class GameLogic
{
    private const string SettlementsFile = "settlements.json";
    private const string RoutesFile = "routes.json";

    internal GameEngine GameEngine { get; }
    private SettlementsMenu SettlementsMenu { get; }
    private RoutesMenu RoutesMenu { get; }

    public GameLogic()
    {
        GameEngine = new GameEngine();
        SettlementsMenu = new SettlementsMenu(GameEngine);
        RoutesMenu = new RoutesMenu(GameEngine);
    }

    public void NewGame()
    {
        GameEngine.InitGameEngine();

        GameMenu();
    }

    public void LoadGame()
    {
        Load();

        GameMenu();
    }

    private void GameLoop()
    {
        Console.WriteLine("---------------");
        GameEngine.ProcessNextRound();

        foreach (var settlement in GameEngine.GetSettlements())
        {
            Console.WriteLine($"{settlement.Name}: {settlement.Type}|({settlement.Population})");
        }

        Save();

        Console.WriteLine("---------------");
    }

    private void Save()
    {
        File.WriteAllText(SettlementsFile, JsonSerializer.Serialize(GameEngine.GetSettlements()));
        File.WriteAllText(RoutesFile, JsonSerializer.Serialize(GameEngine.GetRoutes()));
    }

    private void Load()
    {
        if (!File.Exists(SettlementsFile))
        {
            GameEngine.InitGameEngine();
            return;
        }

        var settlements = JsonSerializer.Deserialize<List<Settlement>>(File.ReadAllText(SettlementsFile));
        var routes = JsonSerializer.Deserialize<List<Route>>(File.ReadAllText(RoutesFile));

        GameEngine.InitGameEngine(settlements, routes);
    }

    public void GameMenu()
    {

        while (true)
        {
            Console.Clear();

            // Menu header
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- Main Menu ---");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            // Menu options
            Console.WriteLine("Select action:");
            Console.WriteLine("1. Routes");
            Console.WriteLine("2. Settlements");
            Console.WriteLine("3. Next round");
            Console.WriteLine("0. Exit");

            var input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    return;
                case "1":
                    RoutesMenu.RoutesMainMenu();
                    break;
                case "2":
                    SettlementsMenu.SettlementsMainMenu();
                    break;
                case "3":
                    GameLoop();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }
}
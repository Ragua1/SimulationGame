using System.Text.Json;
using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class GameLogic
{
    private const string SettlementsFile = "settlements.json";
    private const string RoutesFile = "routes.json";
    
    private GameEngine GameEngine { get; }
    public GameLogic()
    {
        GameEngine = new GameEngine();
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
        var settlements = JsonSerializer.Deserialize<List<Settlement>>(File.ReadAllText(SettlementsFile));
        var routes =  JsonSerializer.Deserialize<List<Route>>(File.ReadAllText(RoutesFile));
        
        GameEngine.InitGameEngine(settlements, routes);
    }

    public void GameMenu()
    {
        Console.Clear();

        while (true)
        {
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
                    RoutesMenu();
                    break;
                case "2":
                    SettlementsMenu();
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

    #region Routes region
    public void RoutesMenu()
    {
        while (true)
        {
            // Menu header
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- Routes Menu ---");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            // Menu options
            Console.WriteLine("Select action:");
            Console.WriteLine("1. Build routes");
            Console.WriteLine("2. Remove routes");
            Console.WriteLine("3. Rout details");
            Console.WriteLine("4. Back to Main Menu");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    BuildRoute();
                    break;
                case "2":
                    RemoveRoutes();
                    break;
                case "3":
                    ViewRoutes();
                    break;
                case "4":
                    GameMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    public void BuildRoute()
    {

        // Menu header
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("--- Build routes ---");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        Console.WriteLine("Choose poin A and point B");

        Console.WriteLine();

        var settlements = GameEngine.GetSettlements();
        settlements.ShowListOfElements();

        Console.WriteLine();

        Console.Write("Point A: ");
        var a = Console.ReadLine();
        Console.Write("Point B: ");
        var b = Console.ReadLine();
        Console.Write("Name: ");
        var name = Console.ReadLine();

        // TODO if route exists, throw exception
        // TODO add input protection

        GameEngine.AddRoad(name, settlements[int.Parse(a) - 1], settlements[int.Parse(b) - 1]);

        GameMenu();
    }

    public void RemoveRoutes()
    {
        // Menu header
        Console.WriteLine(string.Empty);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("--- Remove routes ---");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(string.Empty);

        var routes = GameEngine.GetRoutes();
        if (!routes.Any())
        {
            Console.WriteLine("There are no routes!");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("Whitch route do you want to remove?");

        Console.WriteLine(string.Empty);
        routes.ShowListOfElements();
        Console.WriteLine(string.Empty);

        var x = Console.ReadLine();

        GameEngine.RemoveRoutes(routes[int.Parse(x) - 1]);

        GameMenu();
    }

    public void ViewRoutes()
    {
        // Menu header
        Console.WriteLine(string.Empty);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("--- View routes ---");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        var routes = GameEngine.GetRoutes();
        if (!routes.Any())
        {
            Console.WriteLine("There are no routes!");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("Which route do you want to see?");
        Console.WriteLine();
        routes.ShowListOfElements();
        Console.WriteLine();

        // TODO add input protection

        var x = Console.ReadLine();
        Console.WriteLine();
        Route(routes[int.Parse(x) - 1]);
    }

    public void Route(Route road)
    {
        Console.Write("Name: ");
        Console.WriteLine(road.Name);

        Console.Write("Start: ");
        Console.WriteLine(road.SettlementBegin.Name);

        Console.Write("End: ");
        Console.WriteLine(road.SettlementEnd.Name);

        Console.WriteLine();
        Console.WriteLine("1. Back to Route Menu");
        Console.WriteLine("2. Back to Main Menu");
        while (true)
        {
            switch (Console.ReadLine())
            {
                case "1":
                    RoutesMenu();
                    break;

                case "2":
                    GameMenu();
                    break;

                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }
    #endregion

    #region SettlementsMenu
    public void SettlementsMenu()
    {
        while (true)
        {
            // Menu header
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- Settlements Menu ---");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            var settlements = GameEngine.GetSettlements();
            settlements.ShowListOfElements();
            Console.WriteLine();

            // Menu options
            Console.WriteLine("Select actions:");
            Console.WriteLine("1. Settlement details");
            Console.WriteLine("2. Create settlement");
            Console.WriteLine("3. Back to Main Menu");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    SettlementDetails();
                    break;
                case "2":
                    CreateSettlement();
                    break;
                case "3":
                    GameMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    private void CreateSettlement()
    {
        // Menu header
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("--- Create settlement ---");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        Console.Write("Name: ");
        var name = Console.ReadLine();
        Console.Write("Description: ");
        var description = Console.ReadLine();

        GameEngine.AddSettlement(name, description);

        GameMenu();
    }

    public void SettlementDetails()
    {
        // Menu header
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("--- Settlements Details ---");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        var settlements = GameEngine.GetSettlements();
        if (!settlements.Any())
        {
            Console.WriteLine("There are no settlements!");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("Which settlement do you want to see?");
        Console.WriteLine();
        settlements.ShowListOfElements();
        Console.WriteLine();

        // TODO add input protection

        var x = Console.ReadLine();
        Console.WriteLine();
        Settlement settlement = (settlements[int.Parse(x) - 1]);

        Console.WriteLine($"Name: {settlement.Name}");
        Console.WriteLine($"Description: {settlement.Description}");
        Console.WriteLine($"Population: {settlement.Population}");
        Console.WriteLine($"Routes:");

        List<Route> routes = GameEngine.GetRoutes(settlement);
        if (routes.Count == 0)
        {
            Console.WriteLine("There are no attached routes.");
        }
        else
        {
            routes.ShowListOfElements();
        }

        Console.WriteLine();
        Console.WriteLine("1. Back to main menu");

        switch (Console.ReadLine())
        {
            case "1":
                GameMenu();
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    #endregion
}
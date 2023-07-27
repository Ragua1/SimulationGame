using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class GameLogic
{
    private GameEngine GameEngine { get; }
    public GameLogic()
    {
        GameEngine = new GameEngine();
    }

    public void NewGame()
    {
        GameEngine.AddSettlement("", "");

        GameLoop();
    }

    private void GameLoop()
    {
        GameMenu();
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
            Console.WriteLine("3. Exit");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    RoutesMenu();
                    break;
                case "2":
                    SettlementsMenu();
                    break;
                case "3":
                    Environment.Exit(0);
                    return;
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

        Console.WriteLine(string.Empty);

        Console.Write("Point A: ");
        var a = Console.ReadLine();
        Console.Write("Point B: ");
        var b = Console.ReadLine();
        Console.Write("Name: ");
        var name = Console.ReadLine();

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

            // Menu options
            Console.WriteLine("Select actions:");
            Console.WriteLine("1. Settlement details");
            Console.WriteLine("2. Back to Main Menu");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
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
    }

    #endregion
}
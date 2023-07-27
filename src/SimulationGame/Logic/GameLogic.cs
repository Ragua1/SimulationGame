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
        GameLoop();
    }

    private void GameLoop()
    {
        GameMenu();
    }

    public void GameMenu()
    {
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
            Console.WriteLine("1. Roads");
            Console.WriteLine("2. Settlements");
            Console.WriteLine("3. Exit");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    RoadsMenu();
                    break;
                case "2":
                    SettlementsMenu();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    public void RoadsMenu()
    {
        while (true)
        {
            // Menu header
            Console.WriteLine(  );
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--- Roads Menu ---");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            // Menu options
            Console.WriteLine("Select action:");
            Console.WriteLine("1. Build roads");
            Console.WriteLine("2. Remove roads");
            Console.WriteLine("3. Road details");
            Console.WriteLine("4. Back to Main Menu");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    BuildRoad();
                    break;
                case "2":
                    RemoveRoads();
                    break;
                case "3":
                    ViewRoads();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

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
                    return;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    public void BuildRoad()
    {
        // Menu header
        Console.WriteLine(  );
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("--- Build roads ---");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        Console.WriteLine("Choose poin A and point B");

        Console.WriteLine();

        var settlements = GameEngine.GetSettlements();
        ShowListOfElements(settlements);

        Console.WriteLine(string.Empty);

        Console.Write("Point A: ");
        var a = Console.ReadLine();
        Console.Write("Point B: ");
        var b = Console.ReadLine();
        Console.Write("Name: ");
        var name = Console.ReadLine();

        GameEngine.AddRoad(name, settlements[int.Parse(a)], settlements[int.Parse(b)]);
    }

    public void RemoveRoads()
    {
        // Menu header
        Console.WriteLine(string.Empty);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("--- Remove roads ---");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(string.Empty);

        Console.WriteLine("Whitch road do you want to remove?");

        Console.WriteLine(string.Empty);


        var routes = GameEngine.GetRoutes();
        ShowListOfElements(routes);

        Console.WriteLine(string.Empty);

        var x = Console.ReadLine();

        // TODO remove selected road

    }

    public void ViewRoads()
    {
        // Menu header
        Console.WriteLine(string.Empty);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("--- View roads ---");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        var roads = GameEngine.GetRoutes();
        if (!roads.Any())
        {
            Console.WriteLine("There is not any route!");

            Console.WriteLine();
            return;
        }

        Console.WriteLine("Which road do you want to see?");

        Console.WriteLine();

        ShowListOfElements(roads);

        Console.WriteLine();

        // TODO sellect a road and pass it as an argument to Road();


        var x = Console.ReadLine();

        Road(roads[int.Parse(x)]);
    }

    public void Road(Route road)
    {
        // TODO display details of a selected road

        Console.WriteLine("1. Back to Roads Menu");
        // TODO jump to RoadsMenu
        while (true)
        {
            switch (Console.ReadLine())
            {
                case "1":
                    return;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }

    /// <summary>
    /// 
    /// Show list of elements (roads, settlements, etc.)
    /// </summary>
    /// <param name="elements">Collection of <see cref="Element"/></param>
    private static void ShowListOfElements(IEnumerable<Element> elements)
    {
        for (int i = 0; i < elements.Count(); i++)
        {
            var e = elements.ElementAt(i);
            Console.WriteLine($"{i}: {e.Name}");
        }
    }
}

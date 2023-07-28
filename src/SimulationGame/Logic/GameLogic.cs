using System.Text.Json;
using SimulationGame.Models;
using SimulationGame.UI;

namespace SimulationGame.Logic;

public class GameLogic
{
    // JSON constants
    private const string SettlementsFile = "settlements.json";
    private const string RoutesFile = "routes.json";

    // Save dependency classes instances at the start
    internal GameEngine GameEngine { get; }
    private SettlementsMenu SettlementsMenu { get; }
    private RoutesMenu RoutesMenu { get; }

    public GameLogic()
    {
        GameEngine = new GameEngine();
        SettlementsMenu = new SettlementsMenu(GameEngine);
        RoutesMenu = new RoutesMenu(GameEngine);
    }

    // Called in API
    // Idk what that shit means
    public void NewGame()
    {
        GameEngine.InitGameEngine();
        GameMenu();
    }

    // Called first, when the game starts
    // Loads saved data, if exists
    // Displays Main Menu
    public void LoadGame()
    {
        Load();
        GameMenu();
    }

    // Proceeds to next round
    // Displays changes in game data
    // Saves data
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

    // Serialize data in seperate JSON files (settlements, routes)
    private void Save()
    {
        File.WriteAllText(SettlementsFile, JsonSerializer.Serialize(GameEngine.GetSettlements()));
        File.WriteAllText(RoutesFile, JsonSerializer.Serialize(GameEngine.GetRoutes()));
    }

    // Deserialize data from JSON files, if exists
    // Load data to game
    // Else start the game fresh
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

    // Display main Game Menu - duh...
    // Make sure to complete all methods, before comming back to this menu
    // "I'm Alpha and Omega. The begining and the end."
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


            // Directory
            /* Should I add parsing extension to input ints, instead of strings in directories? Will it matter?
               - With parsing extension I can protect directories with a while loop
               - No need to loop the whole method
               - With parsign extension and while loops I can make sure to escape menus correctly 
               - Reqires hardcoded menu lenght as parameters
               - I could use the parsing extension as an input protection in other methods
               - Gerat. Let's do this */

            int input = Extensions.InputToInt();
            switch (input)
            {
                case 0:
                    Save();
                    Environment.Exit(0);
                    return;
                case 1:
                    RoutesMenu.RoutesMainMenu();
                    break;
                case 2:
                    SettlementsMenu.SettlementsMainMenu();
                    break;
                case 3:
                    GameLoop();
                    break;
                default:
                    Console.WriteLine("Invalid input. Must refer to menu options");
                    break;
            }
        }
    }
}
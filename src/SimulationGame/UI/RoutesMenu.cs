using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Logic;
using SimulationGame.Models;

namespace SimulationGame.UI
{
    internal class RoutesMenu
    {
        internal GameEngine GameEngine { get; }

        internal RoutesMenu(GameEngine gameEngine)
        {
            GameEngine = gameEngine;
        }

        public void RoutesMainMenu()
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
                Console.WriteLine("3. Route details");
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
                        return;
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

            GameEngine.AddRoute(name, settlements[int.Parse(a) - 1], settlements[int.Parse(b) - 1]);

            //GameMenu();
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

            //GameMenu();
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

            Console.WriteLine("Which route do you want to see?\n");
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
            Console.WriteLine($"Name: {road.Name}");

            Console.Write("Start: ");
            Console.WriteLine(road.SettlementBegin.Name);

            Console.Write("End: ");
            Console.WriteLine(road.SettlementEnd.Name);

            Console.WriteLine("\nPress enter\n");
            Console.ReadLine();
        }
    }
}

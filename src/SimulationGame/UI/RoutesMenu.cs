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
            // Menu header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- Routes Menu ---\n");
            Console.ForegroundColor = ConsoleColor.White;

            // Menu options
            Console.WriteLine("Select action:");
            Console.WriteLine("1. Build routes");
            Console.WriteLine("2. Remove routes");
            Console.WriteLine("3. Route details");
            Console.WriteLine("0. Back to Main Menu");

            var input = 4;
            while (input > 3)
            {
                input = Extensions.InputToInt();
                switch (input)
                {
                    case 1:
                        BuildRoute();
                        break;
                    case 2:
                        RemoveRoutes();
                        break;
                    case 3:
                        ViewRoutes();
                        break;
                    case 0:
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- Build routes ---\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Choose poin A and point B\n");

            var settlements = GameEngine.GetSettlements();
            settlements.ShowListOfElements();

            // Build the route
            var a = 0;
            var b = settlements.Count + 1;
            var routes = GameEngine.GetRoutes();
            var routeExists = true;
            while (routeExists)
            {
                // Select point A
                Console.Write("\nPoint A: ");
                a = Extensions.InputToInt();
                while (a > settlements.Count)
                {
                    Console.WriteLine("There's no such settlement. Try again.");
                    Console.Write("\nPoint A: ");
                    a = Extensions.InputToInt();
                }

                // Select point B
                Console.Write("Point B: ");
                b = Extensions.InputToInt();
                while (b > settlements.Count)
                {
                    Console.WriteLine("There's no such settlement. Try again.");
                    Console.Write("\nPoint B: ");
                    b = Extensions.InputToInt();
                }

                // Check if route already exists
                routeExists = CheckIfExists(settlements, routes, a, b);
            }

            Console.Write("Name: ");
            var name = Console.ReadLine();

            // If all passes, add route to database
            GameEngine.AddRoute(name, settlements[a - 1], settlements[b - 1]);
        }

        private bool CheckIfExists(List<Settlement> settlements, List<Route> routes, int pointA, int pointB)
        {
            if (pointA == pointB)
            {
                Console.WriteLine("Point A nad point B must be different");
                return true;
            }

            if (routes.Count > 0)
            {
                // Check equality of the begining and the end of each existing route with input
                foreach (var route in routes)
                {
                    if (route.SettlementBegin.Id != settlements[pointA - 1].Id || route.SettlementEnd.Id != settlements[pointB - 1].Id)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("This route already exists. Try again.\n");
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }

            return false;
        }

        public void RemoveRoutes()
        {
            // Menu header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- Remove routes ---\n");
            Console.ForegroundColor = ConsoleColor.White;

            var routes = GameEngine.GetRoutes();
            if (!routes.Any())
            {
                Console.WriteLine("There are no routes!\n");
                return;
            }

            Console.WriteLine("Whitch route do you want to remove?\n");
            routes.ShowListOfElements();
            Console.WriteLine();

            var x = Extensions.InputToInt();

            GameEngine.RemoveRoute(routes[x - 1]);
        }

        public void ViewRoutes()
        {
            // Menu header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- View routes ---\n");
            Console.ForegroundColor = ConsoleColor.White;

            var routes = GameEngine.GetRoutes();
            if (!routes.Any())
            {
                Console.WriteLine("There are no routes!\n");
                return;
            }

            Console.WriteLine("Which route do you want to see?\n");
            routes.ShowListOfElements();
            Console.WriteLine();

            var x = Extensions.InputToInt();
            Console.WriteLine();
            Route(routes[x - 1]);
        }

        public void Route(Route road)
        {
            Console.WriteLine($"Name: {road.Name}");
            Console.Write($"Start: {road.SettlementBegin.Name}");
            Console.WriteLine($"End: {road.SettlementEnd.Name}");
            Console.Write($"ID: {road.Id}");

            Console.WriteLine("\nPress enter\n");
            Console.ReadLine();
        }

        public void UpgradeRoute()
        {
            throw new NotImplementedException();
        }
    }
}

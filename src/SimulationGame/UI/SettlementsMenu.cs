using SimulationGame.Logic;
using SimulationGame.Models;

namespace SimulationGame.UI
{
    internal class SettlementsMenu
    {
        internal GameEngine GameEngine { get; }

        internal SettlementsMenu(GameEngine gameEngine)
        {
            GameEngine = gameEngine;
        }

        public void SettlementsMainMenu()
        {
            // Menu header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- Settlements Menu ---\n");
            Console.ForegroundColor = ConsoleColor.White;

            var settlements = GameEngine.GetSettlements();
            settlements.ShowListOfElements();

            // Menu options
            Console.WriteLine("\nSelect actions:");
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine("1. Settlement details");
            Console.WriteLine("2. Create settlement");

            var input = 3;
            while (input > 2)
            {
                input = Extensions.InputToInt();
                switch (input)
                {
                    case 1:
                        SettlementDetails();
                        break;
                    case 2:
                        CreateSettlement();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        private void CreateSettlement()
        {
            // Menu header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- Create settlement ---\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Name: ");
            var name = Console.ReadLine();
            Console.Write("Description: ");
            var description = Console.ReadLine();

            GameEngine.AddSettlement(name, description);
        }

        public void SettlementDetails()
        {
            // Menu header
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- Settlements Details ---\n");
            Console.ForegroundColor = ConsoleColor.White;

            var settlements = GameEngine.GetSettlements();
            if (!settlements.Any())
            {
                Console.WriteLine("There are no settlements!\n");
                return;
            }

            Console.WriteLine("Which settlement do you want to see?\n");
            settlements.ShowListOfElements();

            var x = Extensions.InputToInt();
            Settlement settlement = (settlements[x - 1]);

            Console.WriteLine($"\nName: {settlement.Name}");
            Console.WriteLine($"Description: {settlement.Description}");
            Console.WriteLine($"Population: {settlement.Population}");
            Console.WriteLine($"ID: {settlement.Id}");
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

            Console.WriteLine("\nPress enter");
            Console.ReadLine();
        }
    }
}

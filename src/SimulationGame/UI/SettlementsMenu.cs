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

            //GameMenu();
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
            Console.WriteLine("Press enter");
            Console.ReadLine();
        }
    }
}

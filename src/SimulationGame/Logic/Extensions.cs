using SimulationGame.Models;

namespace SimulationGame.Logic
{
    internal static class Extensions
    {
        /// <summary>
        /// Show list of elements (roads, settlements, etc.)
        /// </summary>
        /// <param name="elements">Collection of <see cref="IElement"/></param>
        public static void ShowListOfElements(this IEnumerable<IElement> elements)
        {
            for (int i = 0; i < elements.Count(); i++)
            {
                var e = elements.ElementAt(i);
                Console.WriteLine($"{i + 1}: {e.Name}");
            }
        }

        /// <summary>
        /// Try parse input to integer
        /// </summary>
        /// <returns></returns>
        public static int InputToInt()
        {
            int result = 0;
            bool parse = false;

            while (!parse)
            {
                parse = int.TryParse(Console.ReadLine(), out result);

                if (!parse)
                {
                    Console.WriteLine("Invalid input. Must be whole number.");
                }
            }
            return result;
        }
    }
}

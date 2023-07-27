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
    }
}

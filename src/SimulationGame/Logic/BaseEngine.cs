using SimulationGame.Models;

namespace SimulationGame.Logic;

internal abstract class BaseEngine
{
    // Collection of elements
    protected internal List<IElement> Elements { get; set; } = new ();
    // Return new id for new element
    protected internal int NewId() => Elements.Any()
        ? Elements.Max(x => x.Id) + 1
        : 1;
    
    protected Random Random { get; } = new(DateTime.Now.Millisecond);
}
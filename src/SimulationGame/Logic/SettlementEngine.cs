using System.Runtime.CompilerServices;
using SimulationGame.Models;

[assembly: InternalsVisibleTo("SimulationGame.Test")]
namespace SimulationGame.Logic;

internal class SettlementEngine
{
    public List<Settlement> Settlements { get; } = new List<Settlement>();


    public void AddSettlement(string name, string description, string type, int population)
    {
        Settlements.Add(new Settlement
        {
            Name = name,
            Description = description,
            Type = type,
            Population = population
        });
    }

    public void RemoveSettlement(Settlement settlement)
    {
        Settlements.Remove(settlement);
    }
}

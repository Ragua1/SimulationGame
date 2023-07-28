using SimulationGame.Enums;
using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class SettlementEngine
{
    private List<Settlement> Settlements { get; set; } = new List<Settlement>();
    private Random Random { get; } = new(DateTime.Now.Millisecond);

    public void InitSettlementEngine(List<Settlement>? settlements = null)
    {
        Settlements = settlements ?? new List<Settlement>();
    }

    public void GenerateNew(int id)
    {
        var names = new[] { "Markéta", "Romana", "Maruše", "Dana", "Karlička" };
        var descriptions = new[]
        {
            "Settlement in a desert, built by wealthy oil magnates",
            "Settlement in mountains, previously used to raise goats",
            "Settlement by a large river, that was used to deliver wood",
            "Settlement, built around a castle. Historic location indeed",
            "Settlement with large amount of fields and tractors",
        };

        for (var i = 0; i < 5; i++)
        {
            AddSettlement(names[i], descriptions[i], id);
            id++;
        }
    }
    public void ProcessNextRound()
    {
        foreach (var settlement in Settlements)
        {
            ChceckSettlementSize(settlement);
        }
    }

    public void AddSettlement(string name, string description, int id)
    {
        var settlement = new Settlement
        {
            Name = name,
            Description = description,
            Id = id,
        };

        GeneratePopulation(settlement);

        Settlements.Add(settlement);
    }

    internal void AddSettlement(string name, string description, SettlementTypes type, int population)
    {
        var settlement = new Settlement
        {
            Name = name,
            Description = description,
            Type = type,
            Population = population,
        };

        GeneratePopulation(settlement);

        Settlements.Add(settlement);
    }

    public List<Settlement> GetSettlements()
    {
        return Settlements;
    }

    public void RemoveSettlement(Settlement settlement)
    {
        Settlements.Remove(settlement);
    }

    public void GeneratePopulation(Settlement settlement)
    {
        settlement.Population = Random.Next(1, 100);
    }

    public static void ChceckSettlementSize(Settlement settlement)
    {
        settlement.Type = settlement.Population switch
        {
            > 0 and <= 5 => SettlementTypes.Village,
            > 5 and <= 10 => SettlementTypes.Town,
            _ => SettlementTypes.City,
        };
    }
}

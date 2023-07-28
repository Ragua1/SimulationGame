using SimulationGame.Enums;
using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class SettlementEngine : BaseEngine
{
    // only cast (type) elements to settlements
    // !!!MUST NOT!!! add new settlements to this collection, use Elements collection instead
    private List<Settlement> Settlements
    {
        get => Elements.Cast<Settlement>().ToList();
        set => Elements = value.Cast<IElement>().ToList();
    }

    public void InitSettlementEngine(List<Settlement>? settlements = null)
    {
        Settlements = settlements ?? new List<Settlement>();
    }
    

    public void GenerateNew()
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
            AddSettlement(names[i], descriptions[i]);
        }
    }
    public void ProcessNextRound()
    {
        foreach (var settlement in Settlements)
        {
            CheckSettlementSize(settlement);
        }
    }

    internal void AddSettlement(string name, string description, SettlementTypes type = SettlementTypes.Village, int population = 0)
    {
        var settlement = new Settlement
        {
            Name = name,
            Description = description,
            Type = type,
            Population = population,
            Id = NewId(),
        };

        GeneratePopulation(settlement);

        // new element must be add to the Elements collection, not in the Settlements collection
        Elements.Add(settlement);
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

    public static void CheckSettlementSize(Settlement settlement)
    {
        settlement.Type = settlement.Population switch
        {
            > 0 and <= 5 => SettlementTypes.Village,
            > 5 and <= 10 => SettlementTypes.Town,
            _ => SettlementTypes.City,
        };
    }
}

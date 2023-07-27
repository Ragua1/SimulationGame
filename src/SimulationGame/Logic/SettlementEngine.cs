using SimulationGame.Enums;
using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class SettlementEngine
{
    public List<Settlement> Settlements { get; } = new List<Settlement>();
    private Random Random { get; } = new(DateTime.Now.Millisecond);

    public void GenerateNew()
    {
        string[] names = new string[] { "Markéta", "Romana", "Maruše", "Dana", "Karlička" };
        string[] descriptions = new string[5];

        descriptions[0] = "Settlement in a desert, built by wealthy oil magnates";
        descriptions[1] = "Settlement in mountains, previously used to raise goats";
        descriptions[2] = "Settlement by a large river, that was used to deliver wood";
        descriptions[3] = "Settlement, built around a castle. Historic location indeed";
        descriptions[4] = "Settlement with large amount of fields and tractors";

        for (int i = 0; i < 5; i++)
        {
            AddSettlement(names[i], descriptions[i]);
            
        }
    }
    public void ProcessNextRound()
    {
        foreach (var settlement in Settlements)
        {

        }
    }

    public void AddSettlement(string name, string description)
    {
        var settlement = new Settlement
        {
            Name = name,
            Description = description,
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

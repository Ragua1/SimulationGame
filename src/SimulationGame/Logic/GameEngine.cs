using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class GameEngine
{
    private SettlementEngine SettlementEngine { get; } = new ();
    private List<Route> Routes { get; } = new ();

    public void AddSettlement(string name, string description, string type, int population)
    {
        SettlementEngine.AddSettlement(name, description, type, population);
    }

    public List<Settlement> GetSettlements()
    {
        return SettlementEngine.Settlements;
    }

    public void RemoveSettlement(Settlement settlement) 
    {
        SettlementEngine.RemoveSettlement(settlement);
    }

    public void AddRoad(string name, Settlement settlementBegin, Settlement settlementEnd)
    {
        Routes.Add(new Route
        {
            Name = name,
            SettlementBegin = settlementBegin,
            SettlementEnd = settlementEnd
        });
    }

    public List<Route> GetRoutes(Settlement? settlement = null)
    {
        if (settlement != null)
        {
            return Routes.Where(r => r.SettlementBegin == settlement || r.SettlementEnd == settlement).ToList();
        }
        return Routes;
    }

    public void RemoveRoutes(Route road)
    {
        Routes.Remove(road);
    }
}

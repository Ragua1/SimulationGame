using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class GameEngine
{
    private SettlementEngine SettlementEngine { get; } = new();
    private List<Route> Routes { get; } = new();
    private Random Random { get; } = new(DateTime.Now.Millisecond);

    public void AddSettlement(string name, string description)
    {
        if (SettlementEngine.Settlements.Count == 0)
        {
            SettlementEngine.GenerateNew();
        }
        else
        {
            SettlementEngine.AddSettlement(name, description);
        }

    }

    public List<Settlement> GetSettlements()
    {
        return SettlementEngine.Settlements;
    }

    public void RemoveSettlement(Settlement settlement)
    {
        foreach (var route in Routes.Where(x => x.SettlementBegin == settlement || x.SettlementEnd == settlement))
        {
            RemoveRoutes(route);
        }

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

using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class GameEngine
{
    private SettlementEngine SettlementEngine { get; set; } = new();
    private List<Route> Routes { get; set; } = new();
    private Random Random { get; } = new(DateTime.Now.Millisecond);

    public void ProcessNextRound()
    {
        foreach (var route in Routes) 
        {
            route.SettlementBegin.Population++;
            route.SettlementEnd.Population++;
        }
        
        SettlementEngine.ProcessNextRound();
    }
    
    public void InitGameEngine(List<Settlement>? settlements = null, List<Route>? routes = null)
    {
        SettlementEngine.InitSettlementEngine(settlements);
        Routes = routes ?? new List<Route>();
        
        if (!settlements.Any())
        {
            SettlementEngine.GenerateNew();
        }
    }

    public void AddSettlement(string name, string description)
    {
        SettlementEngine.AddSettlement(name, description);
    }

    public List<Settlement> GetSettlements()
    {
        return SettlementEngine.GetSettlements();
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

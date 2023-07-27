using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class GameEngine
{
    private SettlementEngine SettlementEngine { get; set; } = new();
    private VehicleEngine VehicleEngine { get; set; } = new();
    private List<Route> Routes { get; set; } = new();
    private Random Random { get; } = new(DateTime.Now.Millisecond);

    public void ProcessNextRound()
    {
        foreach (var route in Routes) 
        {
            route.SettlementBegin.Population++;
            route.SettlementEnd.Population++;
        }

        foreach (var variableDestination in VehicleEngine.GetVehicles())
        {
            if (variableDestination.Destination is Settlement settlement)
            {
                var routes = GetRoutes(settlement);
                if (routes.Any())
                {
                    variableDestination.Destination = routes[Random.Next(0, routes.Count)];
                }
            }

            if (variableDestination.Destination is Route route)
            {
                if (Random.Next(1, 2) == 1)
                {
                    variableDestination.Destination = route.SettlementBegin;
                    route.SettlementBegin.Population++;
                }
                else
                {
                    variableDestination.Destination = route.SettlementEnd;
                    route.SettlementEnd.Population++;
                }
            }
        }

        foreach (var settlement in GetSettlements())
        {
            if (Random.Next(1, 10) > 5)
            {
                VehicleEngine.AddVehicle("Vehicle_" + settlement.Name, "Car", settlement);
            }
        }
        
        SettlementEngine.ProcessNextRound();
    }
    
    public void InitGameEngine(List<Settlement>? settlements = null, List<Route>? routes = null)
    {
        SettlementEngine.InitSettlementEngine(settlements);
        Routes = routes ?? new List<Route>();
        
        if (!SettlementEngine.GetSettlements().Any())
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

    public void AddRoute(string name, Settlement settlementBegin, Settlement settlementEnd)
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
    
    public List<Vehicle> GetVehicles()
    {
        return VehicleEngine.GetVehicles();
    }
}

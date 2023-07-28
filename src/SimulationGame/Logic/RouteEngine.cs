using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class RouteEngine : BaseEngine
{
    // maps (type) elements to Routes
    private List<Route> Routes
    {
        get => Elements.Cast<Route>().ToList();
        set => Elements = value.Cast<IElement>().ToList();
    }
    
    public void InitRouteEngine(List<Route>? routes = null)
    {
        Routes = routes ?? new List<Route>();
    }
    
    // Add route with name, begining and the end
    internal void AddRoute(string name, Settlement settlementBegin, Settlement settlementEnd)
    {
        Elements.Add(new Route
        {
            Name = name,
            SettlementBegin = settlementBegin,
            SettlementEnd = settlementEnd,
            Id = NewId(),
        });
    }
    
    // Return a list of routes of a given settlement, or all routes
    internal List<Route> GetRoutes(Settlement? settlement = null)
    {
        return settlement != null 
            ? Routes.Where(r => r.SettlementBegin == settlement || r.SettlementEnd == settlement).ToList() 
            : Routes;
    }
    

    internal void RemoveRoute(Route route)
    {
        Routes.Remove(route);
    }
}
using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class GameEngine
{
    private SettlementEngine SettlementEngine { get; set; } = new();
    private VehicleEngine VehicleEngine { get; set; } = new();
    private List<Route> Routes { get; set; } = new();
    private Random Random { get; } = new(DateTime.Now.Millisecond);
    private int SettlementId { get; set; }
    private int RouteId { get; set; }
    private int VehicleId { get; set; }


    // Next round logic
    /* - Each route should decrees population in startign point and increes population in end point,
         depending on the type of the route (and the amount of cars in the starting point?) If so: 
            - Every car should cary a certain amount of people
            - very road should cary a certain amount of cars, depending on the type of the road
            - Cars should be evenly distributed on the roads, deppending on the road capacity
                - Or should the destination be given randomly?
              That's a lot of shit going on at the same time lol
       - Each settlement has a certain chance to generate a car. This deppends on the type of the settlement 
       - Every construction proceeds to next stage, or compleets 
       - Settlements level up, deppending on its population 
       - If settlement has 0 population, it dies 
       - If settlement levels up to Metropolis, game ends */

    public void ProcessNextRound()
    {
        // TODO: Population changes
        // This one doesn't applies the rules. Its effect is just for testing
        // Cars adds a lot of headaches. Let's apply them later
        foreach (var route in Routes)
        {
            route.SettlementBegin.Population--;
            route.SettlementEnd.Population += 2;
        }

        foreach (var variableDestination in VehicleEngine.GetVehicles())
        {
            // Sellect a random route for each vehicle
            if (variableDestination.Destination is Settlement settlement)
            {
                var routes = GetRoutes(settlement);
                if (routes.Any())
                {
                    variableDestination.Destination = routes[Random.Next(0, routes.Count)];
                }
            }


            // Sellect which direction the vehicle should go

            /* This doesn't make sense
               - Car needs to sellect a route, that goes from the settlement the car is currently in
                    considering that every route is one way
               - Hence each settlements needs to have a list of available cars
               - Each car will sellect a route randomly, deppending if the route begins at the settlement
               - Each car will move from settlement to settlement between turns */

            // Sorry, I don't like your game design, Martin
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

        // Percentige chance for the settlement to generate a car
        // TODO: apply this to every settlement type
        foreach (var settlement in GetSettlements())
        {
            if (Random.Next(1, 10) > 5)
            {
                VehicleEngine.AddVehicle("Vehicle_" + settlement.Name, "Car", settlement);
            }
        }

        SettlementEngine.ProcessNextRound();
    }

    // Sets the start od the game
    // Passes saved data to engines
    // If no settlement is present from the start, generate 5 new settlements
    public void InitGameEngine(List<Settlement>? settlements = null, List<Route>? routes = null)
    {
        SettlementEngine.InitSettlementEngine(settlements);
        Routes = routes ?? new List<Route>();
        SetId();

        if (!SettlementEngine.GetSettlements().Any())
        {
            SettlementId = 1;
            RouteId = 1;
            VehicleId = 1;
            SettlementEngine.GenerateNew(SettlementId);
            SetId();
        }
    }

    // Set starting ID values according to the highest ID of each object type
    private void SetId()
    {
        foreach (Settlement s in GetSettlements())
        {
            if (s.Id > SettlementId)
            {
                SettlementId = s.Id + 1;
            }
        }

        foreach (Route r in GetRoutes())
        {
            if (r.Id > RouteId)
            {
                r.Id = RouteId + 1;
            }
        }

        foreach (Vehicle v in GetVehicles())
        {
            if(v.Id > VehicleId)
            {
                v.Id = VehicleId + 1;
            }
        }
    }

    // Add new settlement with name and description
    // Population generates automatically
    public void AddSettlement(string name, string description)
    {
        SettlementEngine.AddSettlement(name, description, SettlementId);

        SettlementId++;
    }

    // Return a list of settlements
    public List<Settlement> GetSettlements()
    {
        return SettlementEngine.GetSettlements();
    }

    // Remove settlement and all atached routes
    public void RemoveSettlement(Settlement settlement)
    {
        foreach (var route in Routes.Where(x => x.SettlementBegin == settlement || x.SettlementEnd == settlement))
        {
            RemoveRoutes(route);
        }

        SettlementEngine.RemoveSettlement(settlement);
    }

    // Add route with name, begining and the end
    public void AddRoute(string name, Settlement settlementBegin, Settlement settlementEnd)
    {
        Routes.Add(new Route
        {
            Name = name,
            SettlementBegin = settlementBegin,
            SettlementEnd = settlementEnd,
            Id = RouteId
        });

        RouteId++;
    }

    // Return a list of routes of a given settlement, or all routes
    public List<Route> GetRoutes(Settlement? settlement = null)
    {
        if (settlement != null)
        {
            return Routes.Where(r => r.SettlementBegin == settlement || r.SettlementEnd == settlement).ToList();
        }
        return Routes;
    }

    public void RemoveRoutes(Route route)
    {
        Routes.Remove(route);
    }

    public List<Vehicle> GetVehicles()
    {
        return VehicleEngine.GetVehicles();
    }
}

using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class GameEngine
{
    private SettlementEngine SettlementEngine { get; set; } = new();
    private VehicleEngine VehicleEngine { get; set; } = new();
    private RouteEngine RouteEngine { get; set; } = new();
    private Random Random { get; } = new(DateTime.Now.Millisecond);


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
        foreach (var route in RouteEngine.GetRoutes())
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
        RouteEngine.InitRouteEngine(routes);

        if (!SettlementEngine.GetSettlements().Any())
        {
            SettlementEngine.GenerateNew();
        }
    }

    // Add new settlement with name and description
    // Population generates automatically
    public void AddSettlement(string name, string description)
    {
        SettlementEngine.AddSettlement(name, description);
    }

    // Return a list of settlements
    public List<Settlement> GetSettlements()
    {
        return SettlementEngine.GetSettlements();
    }

    // Remove settlement and all atached routes
    public void RemoveSettlement(Settlement settlement)
    {
        foreach (var route in RouteEngine.GetRoutes().Where(x => x.SettlementBegin == settlement || x.SettlementEnd == settlement))
        {
            RemoveRoute(route);
        }

        SettlementEngine.RemoveSettlement(settlement);
    }

    // Add route with name, begining and the end
    public void AddRoute(string name, Settlement settlementBegin, Settlement settlementEnd)
    {
        RouteEngine.AddRoute(name, settlementBegin, settlementEnd);
    }

    // Return a list of routes of a given settlement, or all routes
    public List<Route> GetRoutes(Settlement? settlement = null)
    {
        return RouteEngine.GetRoutes();
    }

    public void RemoveRoute(Route route)
    {
        RouteEngine.RemoveRoute(route);
    }

    public List<Vehicle> GetVehicles()
    {
        return VehicleEngine.GetVehicles();
    }
}

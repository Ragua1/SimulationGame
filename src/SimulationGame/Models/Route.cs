using SimulationGame.Enums;

namespace SimulationGame.Models;

internal class Route : IDestination
{
    public Settlement SettlementBegin { get; set; }
    public Settlement SettlementEnd { get; set; }
    public RouteTypes Type { get; set; }
    public string Name { get; set; }
    public int Id { get; set; }
}

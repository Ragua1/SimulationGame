using SimulationGame.Enums;

namespace SimulationGame.Models;

internal class Route : Element
{
    public Settlement SettlementBegin { get; set; }
    public Settlement SettlementEnd { get; set; }
    public RouteTypes Type { get; set; }
}

﻿using SimulationGame.Enums;

namespace SimulationGame.Models;

internal class Route : IElement
{
    public Settlement SettlementBegin { get; set; }
    public Settlement SettlementEnd { get; set; }
    public RouteTypes Type { get; set; }
    public string Name { get; set; }
}

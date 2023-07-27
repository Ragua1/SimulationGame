﻿using SimulationGame.Enums;

namespace SimulationGame.Models;

internal class Settlement : IElement
{
    public string Description { get; set; }
    public SettlementTypes Type { get; set; }
    public int Population { get; set; }
    public string Name { get; set; }
}

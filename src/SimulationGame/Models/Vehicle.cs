namespace SimulationGame.Models;

public class Vehicle : IElement
{
    public string Name { get; set; }
    public string Type { get; set; }
    public IDestination Destination { get; set; }
    public string? DestinationName => Destination?.Name;
    
}
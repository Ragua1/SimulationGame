using SimulationGame.Models;

namespace SimulationGame.Logic;

internal class VehicleEngine : BaseEngine
{
    // maps (type) elements to Vehicles
    private List<Vehicle> Vehicles
    {
        get => Elements.Cast<Vehicle>().ToList();
        set => Elements = value.Cast<IElement>().ToList();
    }
    //private List<(Vehicle Vehicle, IDestination Destination)> VehicleDestinations { get; set; } = new();

    public List<Vehicle> GetVehicles()
    {
        return Vehicles;
    }
    
    public void AddVehicle(string name, string type, IDestination destination)
    {
        var vehicle = new Vehicle
        {
            Name = name, 
            Type = type, 
            Destination = destination,
            Id = NewId(),
        };
        
        Elements.Add(vehicle);
        // VehicleDestinations.Add((vehicle, destination));
    }
    
    public void MoveVehicles(Vehicle vehicle, IDestination destination)
    {
        // var vd = VehicleDestinations.FirstOrDefault(x => x.Vehicle == vehicle);

        // vd.Destination = destination;
        vehicle.Destination = destination;
    }
    
    
    // public List<(Vehicle Vehicle, IDestination Destination)> GetVehicleDestinations()
    // {
    //     return VehicleDestinations;
    // }
}
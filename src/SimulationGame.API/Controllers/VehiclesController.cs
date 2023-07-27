using Microsoft.AspNetCore.Mvc;
using SimulationGame.Logic;

namespace SimulationGame.API.Controllers;

[ApiController]
[Route("[controller]")]
// http://localhost:5000/Vehicles
public class VehiclesController : Controller
{    
    public static GameLogic? GameLogic { get; set; }
    
    // GET
    [HttpGet(Name = "GetVehicles")]
    public IActionResult Get()
    {
        return GameLogic == null ? NotFound() : Ok(GameLogic.GameEngine.GetVehicles());
    }
}
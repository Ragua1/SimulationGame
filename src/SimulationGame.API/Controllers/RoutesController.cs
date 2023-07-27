using Microsoft.AspNetCore.Mvc;
using SimulationGame.Logic;

namespace SimulationGame.API.Controllers;

[ApiController]
[Route("[controller]")]
// http://localhost:5000/Routes
public class RoutesController : Controller
{
    public static GameLogic? GameLogic { get; set; }
    
    // GET
    [HttpGet(Name = "GetRoutes")]
    public IActionResult Get()
    {
        return GameLogic == null ? NotFound() : Ok(GameLogic.GameEngine.GetRoutes());
    }
}
using Microsoft.AspNetCore.Mvc;
using SimulationGame.Logic;

namespace SimulationGame.API.Controllers;

[ApiController]
[Route("[controller]")]
// http://localhost:5000/Settlements
public class SettlementsController : Controller
{
    public static GameLogic? GameLogic { get; set; }
    
    // GET
    [HttpGet(Name = "GetSettlements")]
    public IActionResult Get()
    {
        return GameLogic == null ? NotFound() : Ok(GameLogic.GameEngine.GetSettlements());
    }
}
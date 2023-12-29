using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomWins.Application.IServices;

namespace RandomWins.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSessionController : ControllerBase
    {
        private readonly IGameSessionService _gamesessionservice;
        private readonly ILogger<GameSessionController> _logger;

        public GameSessionController(IGameSessionService gameSessionService,ILogger<GameSessionController> logger)
        {
            this._gamesessionservice = gameSessionService;
            this._logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCurrentGameSession()
        {
            var currentgamesession = await this._gamesessionservice.GetCurrentGameSessionAsync();
            if(currentgamesession == null) 
            {
                return NotFound();
            }
            return Ok(currentgamesession);
        }
    }
}


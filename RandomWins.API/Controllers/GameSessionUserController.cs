using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomWins.Application.DTOs;
using RandomWins.Application.IServices;

namespace RandomWins.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSessionUserController : ControllerBase
    {
        private readonly IGameSessionUserService _gameSessionUserService;
        private readonly ILogger<GameSessionUserController> _logger;

        public GameSessionUserController(IGameSessionUserService gameSessionUserService,ILogger<GameSessionUserController> logger)
        {
            this._gameSessionUserService = gameSessionUserService;
            this._logger = logger;
        }



        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<IActionResult> PlayGame([FromBody]GameSessionUserRequestDTO gameDetails)
        {
            var result = await this._gameSessionUserService.PlayGameAsync(gameDetails);
            if(result.isSuccess) 
            {
                return Ok(result.dataObject);
            }
            return StatusCode(500, result.message);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllWinners([FromQuery]int recordCount = 10)
        {
            var result = this._gameSessionUserService.GetAllWinners(recordCount);
            if(result.isSuccess) 
            {
                return Ok(result.dataObject);
            }
            return NotFound(result.message);
        }
    }
}




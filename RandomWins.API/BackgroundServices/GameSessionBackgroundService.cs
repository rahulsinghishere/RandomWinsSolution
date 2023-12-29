using RandomWins.Application.IServices;
using RandomWins.Core.Domain;

namespace RandomWins.API.BackgroundServices
{
    public class GameSessionBackgroundService : BackgroundService
    {
        private readonly IGameSessionService _gamesessionservice;
        private readonly ILogger<GameSessionBackgroundService> _logger;

        public GameSessionBackgroundService(IGameSessionService gameSessionService,ILogger<GameSessionBackgroundService> logger)
        {
            this._gamesessionservice = gameSessionService;
            this._logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            try
            {
                //while(!stoppingToken.IsCancellationRequested) 
                //{
                    ////Get Current Session
                    //    //Check Elapsed Time
                    //var currentsession = await _gamesessionservice.GetCurrentGameSessionAsync();
                    //if(currentsession != null && currentsession.EndDateTime > DateTime.Now)
                    //{
                    //    Console.WriteLine($"Current Session ID:{currentsession.Id} and Current Session Name:{currentsession.SessionName}");
                    //}
                    //else
                    //{
                    //    await _gamesessionservice.CreateGameSessionAsync();
                    //}

                    //Timer timer = new Timer((callback) =>
                    //{
                    //    _gamesessionservice.CreateGameSessionAsync().GetAwaiter().GetResult();
                    //}, null, 0, this._gamesessionservice.SessionTime * 1000);

                //}

                while(!stoppingToken.IsCancellationRequested)
                {
                    await _gamesessionservice.EndCurrentGameSession();
                    await _gamesessionservice.CreateGameSessionAsync();
                    await Task.Delay(this._gamesessionservice.SessionTime * 1000);

                }
                
            }
            catch
            {
                throw;
            }

            await Task.CompletedTask;
        }
    }
}


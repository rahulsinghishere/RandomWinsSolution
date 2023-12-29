using Microsoft.Extensions.Logging;
using RandomWins.Application.DTOs;
using RandomWins.Application.IServices;
using RandomWins.Core.Domain;
using RandomWins.Core.SharedKernel;
using RandomWins.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Application.Services
{
    public class GameSessionUserService : IGameSessionUserService,ISubscriber
    {
        private readonly IGenericRepository<GameSessionUser> _genericrepository;
        private readonly IGameSessionUserRepository _gameSessionUserRepository;
        private readonly ILogger<GameSessionUserService> _logger;

        public GameSessionUserService(IGenericRepository<GameSessionUser> genericRepository,IGameSessionUserRepository gameSessionUserRepository, ILogger<GameSessionUserService> logger)
        {
            this._genericrepository = genericRepository;
            this._gameSessionUserRepository = gameSessionUserRepository;
            this._logger = logger;
            
        }

        public (bool isSuccess, dynamic? dataObject, string message) GetAllWinners(int recordCount)
        {
            IEnumerable<GameSessionWinner> users = default!;
            try
            {
                var allusers = this._gameSessionUserRepository.GetUsersWithGameSessions(i => true);

                users = this._gameSessionUserRepository.GetUsersWithGameSessions(i => i.IsWinner == true).OrderByDescending(i=>i.CreatedDate).Take(recordCount).Select(i =>
                {
                    return new GameSessionWinner
                    {
                        GameSessionId = i.GameSessionId,
                        GameSessionName = i.GameSession.SessionName,
                        WinnerFullName = i.UserFullName,
                        WinnerUserEmailAddress = i.UserEmailAddress,
                        WinningNumber = i.RandomNumber
                    };
                });

                if(users != null && users.Count() > 0)
                {
                    return (true, users, "Successfull");
                }
                return (false, null, "No Records Found");
            }
            catch
            {
                throw;
            }
        }

        public async Task<(bool isSuccess, dynamic? dataObject,string message)> PlayGameAsync(GameSessionUserRequestDTO gameSessionUserDTO)
        {

            GameSessionUserResponseDTO? responseDTO = null;
            try
            {
                if (string.IsNullOrWhiteSpace(gameSessionUserDTO.UserEmailAddress))
                {
                    return (false, responseDTO, $"UserEmail is required");
                }
                if (!this._genericrepository.Context.GameSessions.Any(i => i.Id == gameSessionUserDTO.GameSessionId && i.IsActive == true))
                {
                    return (false, responseDTO, $"Session does not Exist or is Expired");
                }
                if (this._genericrepository.Context.GameSessionUsers.Any(i=>i.GameSessionId == gameSessionUserDTO.GameSessionId && i.UserEmailAddress == gameSessionUserDTO.UserEmailAddress))
                {
                    return (false, responseDTO, $"User already Exists in Current Session");
                }
                var dbobject = new GameSessionUser(gameSessionUserDTO.UserEmailAddress,gameSessionUserDTO.UserFullName, gameSessionUserDTO.GameSessionId, GenerateRandomNumber(),createdBy:nameof(GameSessionUserService));

                await this._genericrepository.AddAsync(dbobject);

                bool issuccess = await this._genericrepository.SaveChangesAsync();

                if (issuccess)
                {
                    responseDTO = new GameSessionUserResponseDTO { RandomNumber = dbobject.RandomNumber, RequestDetails = gameSessionUserDTO };
                }
                return (issuccess, responseDTO, "Successfull");

            }
            catch
            {
                throw;
            }
        }

        public async Task ComputeWinners(IEnumerable<int> gameSessionIds)
        {
            foreach(var  gameSessionId in gameSessionIds) 
            {
                await this.ComputeWinners(gameSessionId);
            }
        }

        public async Task ComputeWinners(int gameSessionId)
        {
            if (!this._genericrepository.Context.GameSessions.Any(i => i.Id == gameSessionId))
            {
                throw new Exception($"Session ID: {gameSessionId} does not Exist");
            }

            var getusers = this._genericrepository.Get(i => i.GameSessionId == gameSessionId);
            if(getusers == null || getusers.Count() <= 0)
            {
                return;
            }
            int maxnumber = this._genericrepository.Get(i => i.GameSessionId == gameSessionId).Max(i => i.RandomNumber);
            var winners = getusers.Where(i => i.RandomNumber == maxnumber);
            foreach(var winner in winners)
            {
                winner.SetWinner();
            }

            this._genericrepository.UpdateRange(winners);
            await this._genericrepository.SaveChangesAsync();
        }

        private int GenerateRandomNumber()
        {
            return Random.Shared.Next(0, 100);
        }

        public async Task ExecuteAsync<T>(T entity) where T : BaseEntity
        {
            await this.ComputeWinners(entity.Id);
        }

        public async Task SubscribeAsync(IPublisher publisher)
        {
            await publisher.AddSubscriberAsync(this);

        }

        public async Task UnSubscribeAsync(IPublisher publisher)
        {
            await publisher.RemoveSubscriberAsync(this);
        }
    }
}


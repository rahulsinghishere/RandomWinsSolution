using RandomWins.Application.DTOs;
using RandomWins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Application.IServices
{
    public interface IGameSessionUserService
    {
        public Task<(bool isSuccess,dynamic? dataObject,string message)> PlayGameAsync(GameSessionUserRequestDTO gameSessionUserDTO);
        public (bool isSuccess, dynamic? dataObject, string message) GetAllWinners(int recordCount);
        public Task ComputeWinners(int gameSessionId);
        public Task ComputeWinners(IEnumerable<int> gameSessionIds);
    }
}


using RandomWins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Application.IServices
{
    public interface IGameSessionService
    {
        public Task<GameSession> CreateGameSessionAsync();
        public int SessionTime { get; }
        public Task<GameSession?> GetCurrentGameSessionAsync();
        public Task EndCurrentGameSession();
    }
}


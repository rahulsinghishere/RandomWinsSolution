using RandomWins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Infrastructure.IRepository
{
    public interface IGameSessionUserRepository:IGenericRepository<GameSessionUser>
    {
        public IEnumerable<GameSessionUser> GetUsersWithGameSessions(Func<GameSessionUser, bool> condition);
    }
}

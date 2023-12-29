using Microsoft.EntityFrameworkCore;
using RandomWins.Core.Domain;
using RandomWins.Core.OtherObjects;
using RandomWins.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Infrastructure.Repository
{
    public class GameSessionUserRepository : GenericRepository<GameSessionUser>, IGameSessionUserRepository
    {
        public GameSessionUserRepository(RandomWinsDBContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<GameSessionUser> GetUsersWithGameSessions(Func<GameSessionUser,bool> condition)
        {
            return Context.Set<GameSessionUser>().Include<GameSessionUser, GameSession>(i => i.GameSession).AsNoTracking().Where(condition).ToList();
        }
    }
}


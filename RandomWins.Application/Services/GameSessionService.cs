using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using RandomWins.Application.IServices;
using RandomWins.Core.Domain;
using RandomWins.Core.SharedKernel;
using RandomWins.Infrastructure.IRepository;
using RandomWins.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;

namespace RandomWins.Application.Services
{
    public class GameSessionService : IGameSessionService, IPublisher
    {
        //private readonly IGenericRepository<GameSession> _genericrepository;
        private readonly IServiceProvider _serviceprovider;
        private readonly int _sessionexpirationinterval;
        private readonly ICollection<ISubscriber> _subscribers; 

        public GameSessionService(IServiceProvider serviceProvider,int sessionExpirationInterval)
        {
            this._serviceprovider = serviceProvider;
            this._sessionexpirationinterval = sessionExpirationInterval;
            this._subscribers = new List<ISubscriber>();
        }

        public int SessionTime { get { return _sessionexpirationinterval; } }


        public async Task<GameSession?> GetCurrentGameSessionAsync()
        {
            using (IServiceScope scope = this._serviceprovider.CreateScope())
            {
                var genericrepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<GameSession>>();
                if (genericrepository.Context.GameSessions.Any(i => i.IsActive == true))
                {
                    return await genericrepository.Context.GameSessions.SingleAsync(i => i.IsActive == true);
                }
            }

            return null;
        }


        public async Task<bool> IsCurrentSessionRunning()
        {
            using (IServiceScope scope = _serviceprovider.CreateScope())
            {
                var gamesessionrepo = scope.ServiceProvider.GetRequiredService<IGenericRepository<GameSession>>();
                return await gamesessionrepo.Context.GameSessions.AnyAsync(i => i.IsActive == true);
            }
        }

        public async Task EndCurrentGameSession()
        {

            using (IServiceScope svcscope = _serviceprovider.CreateScope())
            {
                var genericrepository = svcscope.ServiceProvider.GetRequiredService<IGenericRepository<GameSession>>();
                var gamesessionusersvc = svcscope.ServiceProvider.GetRequiredService<IGameSessionUserService>();

                var items = genericrepository.Get(i => i.IsActive == true).ToList();
                foreach(var item in items)
                {
                    item.IsActive = false;
                    item.UpdatedDate = DateTime.Now;
                    item.UpdatedBy = "BackgroundService";
                    genericrepository.Update(item);
                }
                if (await genericrepository.SaveChangesAsync())
                {
                    //Raise Session End Event
                    foreach (var item in items)
                    {
                        await this.PublishAsync(item);
                    }
                        
                }
            }
        }

        public async Task<GameSession> CreateGameSessionAsync()
        {
            using(IServiceScope svcscope = _serviceprovider.CreateScope())
            {
                var genericrepository = svcscope.ServiceProvider.GetRequiredService<IGenericRepository<GameSession>>();
                var gamesessionusersvc = svcscope.ServiceProvider.GetRequiredService<IGameSessionUserService>();
                GameSession newgamesession = new GameSession($"RandomWins_{DateTime.Now.ToFileTimeUtc().ToString()}", DateTime.Now, DateTime.Now.AddSeconds(_sessionexpirationinterval), nameof(GameSessionService),true);
                bool issuccesssessioncreation = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await genericrepository.AddAsync(newgamesession);
                    issuccesssessioncreation = await genericrepository.SaveChangesAsync();

                    scope.Complete();
                }

                if (!issuccesssessioncreation)
                {
                    throw new Exception("GameSession could not be created.");
                }
                return newgamesession;

            }
        }

        public async Task AddSubscriberAsync(ISubscriber newSubscriber)
        {
            this._subscribers.Add(newSubscriber);
            await Task.CompletedTask;
        }

        public async Task RemoveSubscriberAsync(ISubscriber existingSubscriber)
        {
            this._subscribers.Remove(existingSubscriber);
            await Task.CompletedTask;
        }

        public async Task PublishAsync<TType>(TType obj) where TType : BaseEntity
        {
            foreach(var subscriber in this._subscribers) 
            {
                await subscriber.ExecuteAsync(obj);
            }
        }
    }
}





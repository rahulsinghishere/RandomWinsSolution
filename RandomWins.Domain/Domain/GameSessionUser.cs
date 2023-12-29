using RandomWins.Core.OtherObjects;
using RandomWins.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Core.Domain
{
    public class GameSessionUser:BaseEntity,ISubscriber
    {
        public GameSessionUser(string userEmailAddress,string userFullName,int gameSessionId,int randomNumber, string createdBy)
        {
            this.UserEmailAddress = userEmailAddress;
            this.GameSessionId = gameSessionId;
            this.RandomNumber = randomNumber;
            this.CreatedBy = createdBy;
            this.UserFullName = userFullName;
        }
        public string UserEmailAddress { get; init; }
        public string UserFullName { get; init; }
        public int GameSessionId { get; init; }
        public int RandomNumber { get; init; }
        public bool IsWinner { get; private set; } = false;
        public GameSession GameSession { get; set; }

        public async Task ExecuteAsync<T>(T entity) where T : BaseEntity
        {
            await Task.CompletedTask;
        }

        public void SetWinner()
        {
            this.IsWinner = true;
            this.UpdatedDate = DateTime.Now;
            this.UpdatedBy = nameof(GameSessionUser);
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






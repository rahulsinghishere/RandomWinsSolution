using RandomWins.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Core.Domain
{
    public class GameSession:BaseEntity
    {
        private readonly ICollection<ISubscriber> _subscribers;
        public GameSession(string sessionName,DateTime startDateTime,DateTime endDateTime, string createdBy,bool isActive=false)
        {
            this.SessionName = sessionName;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.CreatedBy = createdBy;
            this.IsActive = isActive;
        }
        public string SessionName { get; set; }
        public DateTime StartDateTime { get; init; }
        public DateTime EndDateTime { get; init; }

        public virtual ICollection<GameSessionUser> GameSessionUsers { get; set; }

        
    }
}






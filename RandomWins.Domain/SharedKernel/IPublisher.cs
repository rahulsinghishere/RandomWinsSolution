using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Core.SharedKernel
{
    public interface IPublisher
    {
        public Task PublishAsync<TType>(TType obj) where TType : BaseEntity;
        public Task AddSubscriberAsync(ISubscriber newSubscriber);
        public Task RemoveSubscriberAsync(ISubscriber existingSubscriber);
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Core.SharedKernel
{
    public interface ISubscriber
    {
        public Task ExecuteAsync<TType>(TType entity) where TType : BaseEntity;
        public Task SubscribeAsync(IPublisher publisher);
        public Task UnSubscribeAsync(IPublisher publisher);
    }
}


using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using RandomWins.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Infrastructure.IRepository
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        public RandomWinsDBContext Context { get; }
        public Task AddAsync(T entity);

        public void Update(T entity);

        public void UpdateRange(IEnumerable<T> entities);

        public void Delete(T entity);

        public Task<T?> FindAsync(int id);

        public IEnumerable<T> Get(Func<T, bool> whereCondition);

        public Task<bool> SaveChangesAsync();
    }
}


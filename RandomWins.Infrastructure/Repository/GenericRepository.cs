using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RandomWins.Core.SharedKernel;
using RandomWins.Infrastructure.IRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Infrastructure.Repository
{
    public class GenericRepository<T>:IGenericRepository<T> where T : BaseEntity
    {
        private readonly RandomWinsDBContext _dbcontext;
        private readonly DbSet<T> _entity;

        public RandomWinsDBContext Context
        {
            get { return _dbcontext; }
        }

        public GenericRepository(RandomWinsDBContext dbContext)
        {
            _dbcontext = dbContext;
            _entity = _dbcontext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            EntityEntry<T> entityentry = await _entity.AddAsync(entity);
        }

        public void Update(T entity)
        {
            EntityEntry<T> entityentry = _entity.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _entity.UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            EntityEntry<T> entityentry = _dbcontext.Remove(entity);
        }

        public async Task<T?> FindAsync(int id)
        {
            return await _entity.FindAsync(id);
        }

        public IEnumerable<T> Get(Func<T, bool> whereCondition)
        {
            return _entity.Where(whereCondition).ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbcontext.SaveChangesAsync() > 0;
        }
    }
}


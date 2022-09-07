using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.Domain.Entities;
using Test.Domain.Interfaces;
using Test.Infrastructure.Persistence;

namespace Test.Infrastructure.Repository
{
    public class AsyncRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly TestDbContext _dbContext;
        protected DbSet<T> DbSet { get; set; }

        public AsyncRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual IQueryable<T> Include(params Expression<Func<T, object>>[] children)
        {
            children.ToList().ForEach(x => DbSet.Include(x).Load());
            return DbSet;
        }

        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            var keyValues = new object[] { id };
            return await _dbContext.Set<T>().FindAsync(keyValues, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> GetExpressionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<bool> AnyExpressionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate);
        }
    }
}

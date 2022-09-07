using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Test.Domain.Entities;

namespace Test.Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        Task<T> GetExpressionAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyExpressionAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        IQueryable<T> Include(params Expression<Func<T, object>>[] children);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.ViewModel;

namespace Test.Domain.Interfaces
{
    public interface IGenericService<T>
    {
        Task<Result> AddAsync(T entity);
        Task<Result> UpdateAsync(T entity);
        Task<Result> DeleteAsync(Guid Id);
        Task<Result> GetAllAsync();
        Task<Result> GetAsync(Guid Id);
        Task<Result> Search(string value);
    }
}

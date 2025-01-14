using SampleTaskApp.Models;
using System.Linq.Expressions;

namespace SampleTaskApp.IRepositories
{
    public interface IEfRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> CheckByIdAsync(Expression<Func<T, bool>> exp);
    }
}

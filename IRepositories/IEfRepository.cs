using SampleTaskApp.Models;

namespace SampleTaskApp.IRepositories
{
    public interface IEfRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
       
    }
}

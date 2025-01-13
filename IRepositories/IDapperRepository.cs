namespace SampleTaskApp.IRepositories
{
    public interface IDapperRepository<T> where T : class
    {
        Task<IEnumerable<T>> DapperGetAllAsync();
        Task<T> DapperGetByIdAsync(int id);
        Task DapperAddAsync(T entity);
        Task DapperUpdateAsync(T entity);
        Task DapperDeleteAsync(int id);
    }
}

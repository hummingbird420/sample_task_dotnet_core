namespace SampleTaskApp.IRepositories
{
    public interface IDapperBedsRepository<T> : IDapperRepository<T> where T : class
    {
    }
}

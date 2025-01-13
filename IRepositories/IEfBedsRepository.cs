namespace SampleTaskApp.IRepositories
{
    public interface IEfBedsRepository<T> : IEfRepository<T> where T : class
    {
    }
}

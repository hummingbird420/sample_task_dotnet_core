namespace SampleTaskApp.IRepositories
{
    public interface IEfPatientsRepository<T> : IEfRepository<T> where T : class
    {
    }
}

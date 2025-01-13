namespace SampleTaskApp.IRepositories
{
    public interface IDapperPatientsRepository<T> : IDapperRepository<T> where T : class
    {
    }
}

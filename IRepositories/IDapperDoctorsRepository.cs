namespace SampleTaskApp.IRepositories
{
    public interface IDapperDoctorsRepository<T> : IDapperRepository<T> where T : class
    {
    }
}

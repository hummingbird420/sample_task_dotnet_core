
namespace SampleTaskApp.IRepositories
{
    public interface IDapperUserInfoRepository<T> : IDapperRepository<T> where T : class
    {
        Task<T> GetByAuthCredentialAsync(UserInfo user);
    }
}

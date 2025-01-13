using SampleTaskApp.Models;

namespace SampleTaskApp.IRepositories
{
    public interface IEfUserInfoRepository<T> : IEfRepository<T> where T : class
    {
        Task<T> GetByAuthCredentialAsync(UserInfo login);
    }
}

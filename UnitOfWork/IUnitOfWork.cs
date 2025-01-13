using SampleTaskApp.IRepositories;

namespace SampleTaskApp.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEfRepository<T> GetRepository<T>() where T : class;
        IEfUserInfoRepository<UserInfo> EfUserInfoRepository { get; }
        IEfDoctorsRepository<Doctors> EfDoctorsRepository { get; }
        IEfHospitalsRepository<Hospitals> EfHospitalsRepository { get; }
        IEfBedsRepository<Beds> EfBedsRepository { get; }
        IEfPatientsRepository<Patients> EfPatientsRepository { get; }
        IEfBedsAlotementsRepository<BedsAlotements> EfBedsAlotementsRepository { get; }
        IEfNotificationsRepository<Notifications> EfNotificationsRepository { get; }

        Task<int> CompleteAsync(); // Commits all changes to the database
        IDapperRepository<T> GetDapperRepository<T>() where T : class;
        IDapperUserInfoRepository<UserInfo> DapperUserInfoRepository { get; }
        IDapperDoctorsRepository<Doctors> DapperDoctorsRepository { get; }
        IDapperHospitalsRepository<Hospitals> DapperHospitalsRepository { get; }
        IDapperBedsRepository<Beds> DapperBedsRepository { get; }
        IDapperPatientsRepository<Patients> DapperPatientsRepository { get; }
        IDapperBedsAlotementsRepository<BedsAlotements> DapperBedsAlotementsRepository { get; }
        IDapperNotificationsRepository<Notifications> DapperNotificationsRepository { get; }

        Task<int> DapperCompleteAsync();
    }

}

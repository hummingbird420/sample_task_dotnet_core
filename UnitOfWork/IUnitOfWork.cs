using SampleTaskApp.IRepositories;

namespace SampleTaskApp.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEfRepository<T> GetRepository<T>() where T : class;
        IEfUserInfoRepository<UserInfo> EfUserInfoRepository { get; }
        IEfDoctorsRepository<Doctor> EfDoctorsRepository { get; }
        IEfHospitalsRepository<Hospital> EfHospitalsRepository { get; }
        IEfBedsRepository<Bed> EfBedsRepository { get; }
        IEfPatientsRepository<Patient> EfPatientsRepository { get; }
        IEfBedsAlotementsRepository<BedsAlotement> EfBedsAlotementsRepository { get; }
        IEfNotificationsRepository<Notification> EfNotificationsRepository { get; }

        Task<int> CompleteAsync(); // Commits all changes to the database
        IDapperRepository<T> GetDapperRepository<T>() where T : class;
        IDapperUserInfoRepository<UserInfo> DapperUserInfoRepository { get; }
        IDapperDoctorsRepository<Doctor> DapperDoctorsRepository { get; }
        IDapperHospitalsRepository<Hospital> DapperHospitalsRepository { get; }
        IDapperBedsRepository<Bed> DapperBedsRepository { get; }
        IDapperPatientsRepository<Patient> DapperPatientsRepository { get; }
        IDapperBedsAlotementsRepository<BedsAlotement> DapperBedsAlotementsRepository { get; }
        IDapperNotificationsRepository<Notification> DapperNotificationsRepository { get; }

        Task<int> DapperCompleteAsync();
    }

}

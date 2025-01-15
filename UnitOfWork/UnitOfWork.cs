using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;
using SampleTaskApp.Repositories;
using System.Data;

namespace SampleTaskApp.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleTaskDbContext _context;
        private readonly IDbConnection _dapperConnection;



        public IEfUserInfoRepository<UserInfo> EfUserInfoRepository { get; }      
        public IEfDoctorsRepository<Doctor> EfDoctorsRepository { get; }
        public IEfHospitalsRepository<Hospital> EfHospitalsRepository { get; }
        public IEfBedsRepository<Bed> EfBedsRepository { get; }
        public IEfPatientsRepository<Patient> EfPatientsRepository { get; }
        public IEfBedsAlotementsRepository<BedsAlotement> EfBedsAlotementsRepository { get; }
        public IEfNotificationsRepository<Notification> EfNotificationsRepository { get; }

        // Dapper Repositories
        public IDapperUserInfoRepository<UserInfo> DapperUserInfoRepository { get; }
        public IDapperDoctorsRepository<Doctor> DapperDoctorsRepository { get; }
        public IDapperHospitalsRepository<Hospital> DapperHospitalsRepository { get; }
        public IDapperBedsRepository<Bed> DapperBedsRepository { get; }
        public IDapperPatientsRepository<Patient> DapperPatientsRepository { get; }
        public IDapperBedsAlotementsRepository<BedsAlotement> DapperBedsAlotementsRepository { get; }
        public IDapperNotificationsRepository<Notification> DapperNotificationsRepository { get; }

       

        public UnitOfWork(SampleTaskDbContext context, IDbConnection dapperConnection)
        {
            _context = context;
            _dapperConnection = dapperConnection;

            // Entity Framework Repositories

            EfUserInfoRepository = new EfUserInfoRepository(context);
            EfDoctorsRepository = new EfDoctorsRepository(_context);
            EfHospitalsRepository = new EfHospitalsRepository(_context);
            EfBedsRepository = new EfBedsRepository(_context);
            EfPatientsRepository = new EfPatientsRepository(_context);
            EfBedsAlotementsRepository = new EfBedsAlotementsRepository(_context);
            EfNotificationsRepository = new EfNotificationsRepository(_context);

            // Dapper Repositories
            DapperUserInfoRepository = new DapperUserInfoRepository(_dapperConnection);
            DapperDoctorsRepository = new DapperDoctorsRepository(_dapperConnection);
            DapperHospitalsRepository = new DapperHospitalsRepository(_dapperConnection);
            DapperBedsRepository = new DapperBedsRepository(_dapperConnection);
            DapperPatientsRepository = new DapperPatientsRepository(_dapperConnection);
            DapperBedsAlotementsRepository = new DapperBedsAlotementsRepository(_dapperConnection);
            DapperNotificationsRepository = new DapperNotificationsRepository(_dapperConnection);

           
        }
        public IEfRepository<T> GetRepository<T>() where T : class
        {
            return new EfRepository<T>(_context);
        }
        IDapperRepository<T> IUnitOfWork.GetDapperRepository<T>() where T : class
        {
            return new DapperRepository<T>(_dapperConnection);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<int> DapperCompleteAsync()
        {
            // If Dapper is used with transactions, handle committing them here
            // For example:
            // return _dapperConnection.ExecuteAsync("COMMIT;");
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            _context.Dispose();
            _dapperConnection.Dispose();
        }

        
    }


}

using SampleTaskApp.IRepositories;
using System.Data;
using System.Data.Common;

namespace SampleTaskApp.Repositories
{
    public class DapperDoctorsRepository : DapperRepository<Doctors>, IDapperDoctorsRepository<Doctors>
    {
        private readonly IDbConnection _dbConnection;
        public DapperDoctorsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}

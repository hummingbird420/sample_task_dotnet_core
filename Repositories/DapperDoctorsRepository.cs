using SampleTaskApp.IRepositories;
using System.Data;
using System.Data.Common;

namespace SampleTaskApp.Repositories
{
    public class DapperDoctorsRepository : DapperRepository<Doctor>, IDapperDoctorsRepository<Doctor>
    {
        private readonly IDbConnection _dbConnection;
        public DapperDoctorsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}

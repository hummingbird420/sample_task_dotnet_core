using SampleTaskApp.IRepositories;
using System.Data;
using System.Data.Common;

namespace SampleTaskApp.Repositories
{
    public class DapperPatientsRepository : DapperRepository<Patient>, IDapperPatientsRepository<Patient>
    {
        private readonly IDbConnection _dbConnection;
        public DapperPatientsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}

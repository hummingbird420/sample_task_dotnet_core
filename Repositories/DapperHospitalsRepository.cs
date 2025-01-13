using SampleTaskApp.IRepositories;
using System.Data;
using System.Data.Common;

namespace SampleTaskApp.Repositories
{
    public class DapperHospitalsRepository : DapperRepository<Hospitals>, IDapperHospitalsRepository<Hospitals>
    {
        private readonly IDbConnection _dbConnection;
        public DapperHospitalsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}

using SampleTaskApp.IRepositories;
using System.Data;
using System.Data.Common;

namespace SampleTaskApp.Repositories
{
    public class DapperBedsRepository : DapperRepository<Beds>, IDapperBedsRepository<Beds>
    {
        private readonly IDbConnection _dbConnection;
        public DapperBedsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}

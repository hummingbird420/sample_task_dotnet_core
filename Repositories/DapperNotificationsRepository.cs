using SampleTaskApp.IRepositories;
using System.Data;
using System.Data.Common;

namespace SampleTaskApp.Repositories
{
    public class DapperNotificationsRepository : DapperRepository<Notifications>, IDapperNotificationsRepository<Notifications>
    {
        private readonly IDbConnection _dbConnection;
        public DapperNotificationsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}

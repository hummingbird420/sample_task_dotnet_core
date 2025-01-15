using SampleTaskApp.IRepositories;
using System.Data;
using System.Data.Common;

namespace SampleTaskApp.Repositories
{
    public class DapperNotificationsRepository : DapperRepository<Notification>, IDapperNotificationsRepository<Notification>
    {
        private readonly IDbConnection _dbConnection;
        public DapperNotificationsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}

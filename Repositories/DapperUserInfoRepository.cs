using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SampleTaskApp.IRepositories;
using System.Data;
using System.Data.Common;

namespace SampleTaskApp.Repositories
{
    public class DapperUserInfoRepository : DapperRepository<UserInfo>, IDapperUserInfoRepository<UserInfo>
    {
        private readonly IDbConnection _dbConnection;
        public DapperUserInfoRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<UserInfo> GetByAuthCredentialAsync(UserInfo user)
        {
            var sql = "SELECT * FROM UserInfos WHERE Username = @Username AND Password = @Password";
            var parameters = new { Username = user.UserName, Password = user.Password };
            return await _dbConnection.QuerySingleOrDefaultAsync<UserInfo>(sql, parameters);
        }
    }
}

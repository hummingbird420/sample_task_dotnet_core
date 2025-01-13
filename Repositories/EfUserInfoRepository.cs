using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;
using System.Data;

namespace SampleTaskApp.Repositories
{
    public class EfUserInfoRepository : EfRepository<UserInfo>, IEfUserInfoRepository<UserInfo>
    {
        private readonly DbContext _context;
        public EfUserInfoRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserInfo> GetByAuthCredentialAsync(UserInfo login)
        {
            return await _context.Set<UserInfo>()
             .FirstOrDefaultAsync(u => u.UserName == login.UserName && u.Password == login.Password);
        }

       
    }
}

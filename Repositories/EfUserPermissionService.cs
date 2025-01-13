using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;

namespace SampleTaskApp.Repositories
{
    public class EfUserPermissionService : IUserPermissionService
    {
        private readonly SampleTaskDbContext _context;

        public EfUserPermissionService(SampleTaskDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasPermissionForActionAsync(int userId, string conrollerName, string actionName, string methodName)
        {
            // Fetch user permissions from the database (example)
            var userPermissions = await _context.UserPermissions
                .Where(p => p.UserId == userId && p.ControllerName==conrollerName && p.MethodName==methodName)
                .ToListAsync();

            // Check if the user has permission for the specific action
            return userPermissions.Count > 0;
        }
    }
}

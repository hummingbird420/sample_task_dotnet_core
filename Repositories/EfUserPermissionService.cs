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

        public async Task<bool> HasPermissionForActionAsync(int userId, string controllerName, string actionName, string methodName)
        {
            // Determine the required permissions based on the HTTP method
            bool isRetrieve = methodName.ToUpper() == "GET";
            bool isCreate = methodName.ToUpper() == "POST";
            bool isEdit = methodName.ToUpper() == "PUT";
            bool isDelete = methodName.ToUpper() == "DELETE";

            // Fetch user permissions from the database
            var hasPermission = await (from u in _context.UserInfos
                                       join p in _context.UserPermissions
                                       on u.Id equals p.UserId into up
                                       from p in up.DefaultIfEmpty()
                                       join s in _context.SystemPageAndActions
                                       on p.PageId equals s.PageId into sp
                                       from s in sp.DefaultIfEmpty()
                                       where u.Role == "Admin" ||
                                             (u.Id == userId &&
                                              s.ControllerName == controllerName &&
                                              ((isRetrieve && p.IsRetrieve) ||
                                               (isCreate && p.IsCreate) ||
                                               (isEdit && p.IsEdit) ||
                                               (isDelete && p.IsDelete)))
                                       select u.Id)
                                       .AnyAsync(); // Use AnyAsync for better performance when checking existence

            return hasPermission;
        }

    }
}

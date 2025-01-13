namespace SampleTaskApp.IRepositories
{
    public interface IUserPermissionService
    {
        Task<bool> HasPermissionForActionAsync(int userId, string controllerName, string actionName, string methodName);
    }
}

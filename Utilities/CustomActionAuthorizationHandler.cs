using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using NuGet.Protocol;
using SampleTaskApp.IRepositories;

namespace SampleTaskApp.Utilities
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute()
        {
            Policy = "CustomActionPolicy";  
           
        }

       
    }
    public class CustomActionAuthorizationRequirement : IAuthorizationRequirement
    {
       

        public CustomActionAuthorizationRequirement()
        {
            
        }
    }
    public class CustomActionAuthorizationHandler : AuthorizationHandler<CustomActionAuthorizationRequirement>
    {
        private readonly IUserPermissionService _userPermissionService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomActionAuthorizationHandler(IUserPermissionService userPermissionService, IHttpContextAccessor httpContextAccessor)
        {
            _userPermissionService = userPermissionService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomActionAuthorizationRequirement requirement)
        {
            var endpoint = _httpContextAccessor.HttpContext.GetEndpoint();
            var descriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            var controllerName = descriptor.ControllerName;
            var actionName = descriptor.ActionName;
            var httpMethod = _httpContextAccessor.HttpContext.Request.Method;

            // Extract UserId from the JWT token
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            // UserId is missing
            if (string.IsNullOrEmpty(userIdClaim))
            {
                SetUnauthorizedResponse();
                context.Fail(); 
                return;
            }
            // Invalid UserId
            if (!int.TryParse(userIdClaim, out int userId))
            {
                SetUnauthorizedResponse();
                context.Fail(); 
                return;
            }

            // Query the database or service for the user's permissions
            var hasPermission = await _userPermissionService.HasPermissionForActionAsync(userId, controllerName, actionName, httpMethod);
            // User is authorized for this action
            if (hasPermission)
            {
                context.Succeed(requirement); 
            }
            // User is not authorized for this action
            else
            {
                SetUnauthorizedResponse();
                context.Fail(); 
            }
        }

        private void SetUnauthorizedResponse()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
   
}

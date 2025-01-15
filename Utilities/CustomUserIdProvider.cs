using Microsoft.AspNetCore.SignalR;

namespace SampleTaskApp.Utilities
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("userIds")?.Value; 
        }
    }
}

using Microsoft.AspNetCore.SignalR;

namespace SampleTaskApp.Utilities
{
    public class NotificationHub : Hub
    {
        // Method to send notifications to a specific user
        public async Task SendNotificationToUser(string[] userIds, string message)
        {
            await Clients.Users(userIds).SendAsync("ReceiveNotification", message);
        }
    }
}

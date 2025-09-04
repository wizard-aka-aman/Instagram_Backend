using Microsoft.AspNetCore.SignalR;

namespace Instagram.Model.NotificationRepo
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string groupName, string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, groupName, message);
            await Clients.Group(user).SendAsync("ReceiveMessage", user, groupName, message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}

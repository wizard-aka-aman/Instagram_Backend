using Microsoft.AspNetCore.SignalR; 

namespace Instagram.Model.Chat
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, groupName, message);
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
        public async Task SendReaction(int messageId, string groupName, string reaction)
        { 
            // Send updated reaction to the group in real-time
            await Clients.Group(groupName).SendAsync("ReceiveReaction", messageId, reaction);
        }

    }
}

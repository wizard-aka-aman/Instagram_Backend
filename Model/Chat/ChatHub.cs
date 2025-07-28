using Microsoft.AspNetCore.SignalR;

namespace Instagram.Model.Chat
{
    public class ChatHub : Hub
    {
        private readonly InstagramContext _context;

        public ChatHub(InstagramContext context)
        {
            _context = context;
        }
        public async Task SendMessage(string groupName, string user, string message)
        {
            // 1. Save message to DB (pseudo-code)
            ChatMessage cm = new ChatMessage
            {
                GroupName = groupName,
                Sender = user,
                Message = message,
                Reaction = null, // Initialize reaction as null
                IsDeleted = false,// Initialize as not deleted
                SentAt = DateTime.Now
            };
            _context.Messages.Add(cm);
            await _context.SaveChangesAsync(); 
            
            // 2. Send to both sender and group with ID
            await Clients.Group(groupName).SendAsync("ReceiveMessage", cm.Id, user, groupName, message);
            await Clients.Group(user).SendAsync("ReceiveMessage", cm.Id, user, groupName, message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SendReaction(int messageId, string groupName, string? reaction)
        {
            try
            {
                Console.WriteLine($"[SendReaction] messageId: {messageId}, groupName: {groupName}, reaction: {reaction}");
                await Clients.Group(groupName).SendAsync("ReceiveReaction", messageId, reaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SendReaction Error] {ex.Message}");
                throw;
            }
        }

        public async Task UnSend(int messageId, string groupName)
        {
            await Clients.Group(groupName).SendAsync("RecieveUnSend", messageId);
        }

    }
}

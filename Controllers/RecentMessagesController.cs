using Instagram.Model;
using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecentMessagesController : Controller
    {
        private readonly InstagramContext _context;

        public RecentMessagesController(InstagramContext context)
        {
            _context = context;
        }
        [HttpPost("save-recent-message")]
        public async Task SaveRecentMessage([FromBody] RecentMessageDto request)
        {
            var recent = await _context.RecentMessages
                .FirstOrDefaultAsync(r => r.Username == request.ReceiverUsername&& r.LoggedInUsername == request.SenderUsername);

            if (recent == null)
            {
                // Create new entry
                var receiverUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.ReceiverUsername);
                recent = new RecentMessages
                {
                    Username = request.ReceiverUsername,
                    FullName = receiverUser.FullName,
                    ProfilePicture = receiverUser.ProfilePicture !=null ?Convert.ToBase64String(receiverUser.ProfilePicture):null,
                    LoggedInUsername = request.SenderUsername,
                    LastMessage = request.Message
                };
                _context.RecentMessages.Add(recent);
            }
            else
            {
                // Update existing
                recent.LastMessage = request.Message;
                recent.LastMessageDateTime = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }

        [HttpGet("recent-messages/{username}")]
        public async Task<IActionResult> GetRecentMessages(string username)
        {
            var recentMessages = await _context.RecentMessages
                .Where(r => r.LoggedInUsername == username)
                .OrderByDescending(r => r.LastMessageDateTime)
                .ToListAsync();

            return Ok(recentMessages);
        }


    }
}

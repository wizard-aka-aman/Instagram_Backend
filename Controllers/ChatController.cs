using Instagram.Model;
using Instagram.Model.Chat;
using Instagram.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly InstagramContext _context;

        public ChatController(InstagramContext context)
        {
            _context = context;
        }

        [HttpGet("{groupName}")]
        public async Task<IActionResult> GetMessages(string groupName)
        {
            var messages = await _context.Messages
                .Where(m => m.GroupName == groupName)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
            return Ok(messages);
        }

        [HttpGet("{groupName}/{reciver}")]
        public async Task<IActionResult> GetMessages(string groupName, string reciver)
        {
            var messages = await _context.Messages.Where(e => e.GroupName == groupName && e.Sender == reciver && !e.IsDeleted || e.GroupName == reciver && e.Sender == groupName && !e.IsDeleted).ToListAsync();
            //var messages1 = await _context.Messages.Where().ToListAsync();
            return Ok(messages);
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteMessage([FromBody] int messageid)
        {
            var existingMessage = await _context.Messages.FindAsync(messageid);
            if (existingMessage == null) return NotFound();
            existingMessage.IsDeleted = true;
            _context.Messages.Update(existingMessage);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage([FromBody] ChatMessage message)
        {
            message.SentAt = DateTime.Now;
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("reaction")]
        public async Task<IActionResult> AddReaction(ReactionDto dto)
        {
            var message = await _context.Messages.FindAsync(dto.messageid);
            if (message == null) return NotFound();

            message.Reaction = dto.reaction;
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }

}

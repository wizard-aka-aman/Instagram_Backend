using Instagram.Model;
using Instagram.Model.DTO;
using Instagram.Model.NotificationRepo;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly InstagramContext _societyContext;
        private readonly INotificationRepository _notification;
        public NotificationController(InstagramContext societyContext, INotificationRepository notification)
        {
            _societyContext = societyContext;
            _notification = notification;
        }

        [HttpGet]
        [Route("getAllNotification/{username}")]
        public async Task<IActionResult> GetFollowers(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            var followers = _notification.GetAllNotification(username);

            return Ok(followers);
        }

        [HttpGet]
        [Route("SeenNotification/{LoggedInUser}")]
        public async Task<IActionResult> SeenNotification(string LoggedInUser)
        {
            if (string.IsNullOrEmpty(LoggedInUser))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            var followers = await _notification.SeenNotification(LoggedInUser);

            return Ok(followers);
        }
        [HttpPost] 
        [Route("AddNotification")]
        public async Task<IActionResult> AddNotification( [FromBody]AddNotificationDto dto)
        {
            if (string.IsNullOrEmpty(dto.UserName))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            var followers = await _notification.AddNotification(dto);

            return Ok(followers);
        }
    }
}

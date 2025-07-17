using Instagram.Model.DTO;
using Instagram.Model.Tables;
using Instagram.Model.UsersRepo;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUserName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            // Assuming you have a method to get user by username
            UsersDto? user = await _usersRepository.GetUserByUserName(username);

            if (user == null)
            {
                return NotFound($"User with username '{username}' not found.");
            }
            return Ok(user);
        }
        [HttpPut("put/{username}")]
        public async Task<IActionResult> EditUserByUserName(string username, [FromBody] UsersDto dto)
        {
            if (string.IsNullOrEmpty(username) || dto == null)
            {
                return BadRequest("Username or user data cannot be null or empty.");
            }

            UsersDto? updatedUser = await _usersRepository.EditUsersByUserName(username, dto);
            if (updatedUser == null)
            {
                return NotFound($"User with username '{username}' not found.");
            }
            return Ok(updatedUser);
        }
        [HttpPost("profilepicture/{username}")]
        public async Task<IActionResult> UpdateProfilePictureByUserName(string username, IFormFile filecollection)
        {
            if (string.IsNullOrEmpty(username) || filecollection == null)
            {
                return BadRequest("Username or file cannot be null or empty.");
            }
            bool? updatedUser = await _usersRepository.UpdateProfilePictureByUserName(username, filecollection);
            if (updatedUser == false)
            {
                return NotFound($"User with username '{username}' not found.");
            }
            return Ok(updatedUser);
        }
        [HttpDelete("profilepicture/{username}")]
        public async Task<IActionResult> RemoveProfilePictureByUserName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            bool? removed = await _usersRepository.RemoveProfilePictureByUserName(username);
            if (removed == false)
            {
                return NotFound($"User with username '{username}' not found.");
            }
            return Ok(removed);
        }
    }
}

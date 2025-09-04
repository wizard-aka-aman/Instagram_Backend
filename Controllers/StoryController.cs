using Instagram.Model.DTO;
using Instagram.Model.StoryRepo;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : Controller
    {
        private readonly IStoryRepository _storyRepository;
        public StoryController(IStoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }
        [HttpGet("GetStoriesByUser/{username}")]
        public async Task<IActionResult> GetStoriesByUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { message = "Username cannot be null or empty." });
            }
            var stories = await _storyRepository.GetStoriesByUser(username);
            if (stories == null)
            {
                return NotFound(new { message = "No stories found for this user." });
            }
            return Ok(stories);
        }
        [HttpGet("GetPersonalStories/{username}")]
        public async Task<IActionResult> GetPersonalStories(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { message = "Username cannot be null or empty." });
            }
            var stories = await _storyRepository.GetPersonalStories(username);
            if (stories == null)
            {
                return NotFound(new { message = "No stories found for this user." });
            }
            return Ok(stories);
        }
        [HttpGet("GetStoryViewers/{username}")]
        public async Task<IActionResult> GetStoryViewers(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { message = "Username cannot be null or empty." });
            }
            var stories = await _storyRepository.GetStoryViewers( username);
            if (stories == null)
            {
                return NotFound(new { message = "No stories found for this user." });
            }
            return Ok(stories);
        }
        [HttpPost("AddStory")]
        public async Task<IActionResult> AddStory([FromForm] StoryDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Username) || dto.imageFile == null)
            {
                return BadRequest(new { message = "Invalid story data." });
            }
            var result = await _storyRepository.AddStory(dto);
            if (result == null || !result.Value)
            {
                return BadRequest(new { message = "Failed to add story." });
            }
            return Ok(new { message = "Story added successfully." });
        }
        [HttpPost("seen")]
        public async Task<IActionResult> MarkAsSeen([FromBody] SeenDto dto)
        {
            var result = await _storyRepository.MarkStoryAsSeen(dto.StoryId, dto.SeenByUsername);
            return result ? Ok() : BadRequest();
        }
        [HttpGet("IsStoryAvailable/{username}")]
        public async Task<IActionResult> IsStoryAvailable(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { message = "Username cannot be null or empty." });
            }
            var story = await _storyRepository.IsStoryAvailable(username);

            return Ok(story);
        }
        [HttpGet("DisplayPostHome/{username}")]
        public async Task<IActionResult> DisplayPostHome(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { message = "Username cannot be null or empty." });
            }
            var story = await _storyRepository.DisplayPostHome(username);

            return Ok(story);
        }




    }
}

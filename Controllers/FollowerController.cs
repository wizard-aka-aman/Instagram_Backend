using Instagram.Model.DTO;
using Instagram.Model.FollowRepo;
using Instagram.Model.PostsRepo;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FollowerController : Controller
    {
        private readonly IFollowRepository _followRepository;
        public FollowerController(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }
        [HttpPost]
        [Route("follow")]
        public async Task<IActionResult> FollowUser(FollowDto dto)
        {
            if (string.IsNullOrEmpty(dto.followerUsername) || string.IsNullOrEmpty(dto.followingUsername))
            {
                return BadRequest("Follower or following username cannot be null or empty.");
            }
            bool isFollowed = await _followRepository.FollowUserAsync(dto.followerUsername, dto.followingUsername);
            if (!isFollowed)
            {
                return BadRequest("Failed to follow user.");
            }
            return Ok(new { message = "User followed successfully." });
        }
        [HttpGet]
        [Route("getfollower/{username}")]
        public async Task<IActionResult> GetFollowers(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            var followers = await _followRepository.GetFollowersAsync(username);
             
            return Ok(followers);
        } 
        [HttpGet]
        [Route("getfollowing/{username}")]
        public async Task<IActionResult> GetFollowing(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be null or empty.");
            }
            var followers = await _followRepository.GetFollowingAsync(username);
             
            return Ok(followers);
        }
        [HttpPost("unfollow")]
        public async Task<IActionResult> UnfollowUser(FollowDto dto)
        {
            if (string.IsNullOrEmpty(dto.followerUsername) || string.IsNullOrEmpty(dto.followingUsername))
            {
                return BadRequest("Follower or following username cannot be null or empty.");
            }
            bool isUnfollowed = await _followRepository.UnfollowUserAsync(dto.followerUsername, dto.followingUsername);
            if (!isUnfollowed)
            {
                return BadRequest("Failed to unfollow user.");
            }
            return Ok(new { message = "User unfollowed successfully." });
        }
        [HttpGet("isfollowing/{followerUsername}/{followingUsername}")]
        public async Task<IActionResult> IsFollowing(string followerUsername, string followingUsername)
        {
            if (string.IsNullOrEmpty(followerUsername) || string.IsNullOrEmpty(followingUsername))
            {
                return BadRequest("Follower or following username cannot be null or empty.");
            }
            bool isFollowing = await _followRepository.IsFollowingAsync(followerUsername, followingUsername);
            return Ok(isFollowing);
        }
    }
}

using Instagram.Model.DTO;
using Instagram.Model.PostsRepo;
using Instagram.Model.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _postsRepository;
        public PostsController(IPostRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromForm] PostsDto post)
        {
            if (post == null)
            {
                return BadRequest(new { message = "Post cannot be null." });
            }
            bool isCreated = await _postsRepository.CreatePostAsync(post);
            if (!isCreated)
            {
                return BadRequest(new { message = "Failed to create post." });
            }
            return Ok(new { message = "Post created successfully." });
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GetAllPostsByUserNameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { message = "Username cannot be null or empty." });
            }
            var posts = await _postsRepository.GetAllPostsByUserNameAsync(username);


            var postDtos = posts.Select(p => new PostDto
            {
                PostId = p.PostId,
                Caption = p.Caption,
                ImageUrl = "data:image/png;base64," + p.ImageUrl,
                VideoUrl = p.VideoUrl,
                Location = p.Location,
                LikesCount = p.LikesCount,
                CommentsCount = p.CommentsCount,
                CreatedAt = p.CreatedAt,
                UserName = p.User?.UserName ?? "Unknown"
            });

            return Ok(postDtos); 
        }
    }
}

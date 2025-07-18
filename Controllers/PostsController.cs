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
                ImageUrl =  p.ImageUrl,
                VideoUrl = p.VideoUrl,
                Location = p.Location,
                LikesCount = p.LikesCount,
                CommentsCount = p.CommentsCount,
                CreatedAt = p.CreatedAt,
                UserName = p.User?.UserName ?? "Unknown"
            });

            return Ok(postDtos); 
        }
        [HttpGet("post/{postId}/{username}")]
        public async Task<IActionResult> GetPostByIdWithUserNameAsync(int postId, string username)
        {
            if (postId <= 0 || string.IsNullOrEmpty(username))
            {
                return BadRequest(new { message = "Invalid post ID or username." });
            }
            DisplayPostDto posts = await _postsRepository.GetPostByIdWithUserNameAsync(postId, username);

 
            return Ok(posts);
        }
        [HttpPost("like")]
        public async Task<IActionResult> LikePost([FromBody] LikeAndUnLikeDto dto )
        {
            if (string.IsNullOrEmpty(dto.postUsername) || string.IsNullOrEmpty(dto.likedBy) || dto.postId <= 0)
            {
                return BadRequest(new { message = "Invalid post username, liked by username or post ID." });
            }
            bool isLiked = await _postsRepository.LikePost(dto.postUsername, dto.likedBy, dto.postId);
            if (!isLiked)
            {
                return BadRequest(new { message = "Failed to like the post." });
            }
            return Ok(new { message = "Post liked successfully." });
        }
        [HttpPost("unlike")]
        public async Task<IActionResult> UnlikePost([FromBody] LikeAndUnLikeDto dto)
        {
            if (string.IsNullOrEmpty(dto.postUsername) || string.IsNullOrEmpty(dto.likedBy) || dto.postId <= 0)
            {
                return BadRequest(new { message = "Invalid post username, liked by username or post ID." });
            }
            bool isUnliked = await _postsRepository.UnLikePost(dto.postUsername, dto.likedBy, dto.postId);
            if (!isUnliked)
            {
                return BadRequest(new { message = "Failed to unlike the post." });
            }
            return Ok(new { message = "Post unliked successfully." });
        }
        [HttpPost("comment")]
        public async Task<IActionResult> AddCommentByPostIdWithUserName([FromBody] CommentDtoWithPostId dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.UserName) || dto.PostId <= 0 || string.IsNullOrEmpty(dto.CommentText))
            {
                return BadRequest(new { message = "Invalid comment data." });
            }
            bool isCommented = await _postsRepository.AddCommentByPostIdWithUserName(dto);
            if (!isCommented)
            {
                return BadRequest(new { message = "Failed to add comment." });
            }
            return Ok(new { message = "Comment added successfully." });
        }
    }
}

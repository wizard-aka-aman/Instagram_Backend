using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Instagram.Model;
using Instagram.Model.Tables;
using Instagram.Model.DTO;
using Microsoft.EntityFrameworkCore;
using Instagram.Model.ReelRepo;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;
        private readonly InstagramContext _context;
        private readonly IReelRepository _reelRepository;

        public VideosController(Cloudinary cloudinary, InstagramContext context , IReelRepository reelRepository)
        {
            _cloudinary = cloudinary;
            _context = context;
            _reelRepository = reelRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadVideo([FromForm]  VideoDto file)
        {
            if (file == null || file.file.Length == 0)
                return BadRequest("No file uploaded");

            using var stream = file.file.OpenReadStream();
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(file.file.FileName, stream)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                return StatusCode(500, uploadResult.Error.Message);

            Users user =await _context.Users.FirstOrDefaultAsync(e => e.UserName == file.username);
            // Optionally, you can save the video metadata to your database
            var video = new CloudinaryDB
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString(),
                CreatedAt = DateTime.Now,
                CommentCount = 0,
                LikeCount = 0,
                UserName = file.username,
                ProfilePicture = user.ProfilePicture !=null? Convert.ToBase64String(user.ProfilePicture) : null,
                Description = file.description
            };
            _context.CloudinaryDB.Add(video);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString()
            });
        }

        //[HttpGet("{publicId}")]
        //public IActionResult GetVideoUrl(string publicId)
        //{
        //    if (string.IsNullOrEmpty(publicId))
        //        return BadRequest("Public ID cannot be null or empty");
        //    publicId = "my_videos/" + publicId;
        //    var video = _context.CloudinaryDB.FirstOrDefault(v => v.PublicId == publicId);
        //    if (video == null)
        //        return NotFound("Video not found");

        //    return Ok(new { Url = video.Url });
        //}

        [HttpGet("GetReelByUsername/{username}")]
        public async Task<IActionResult> GetReelByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest("Public ID cannot be null or empty");
            
            var video = await _context.CloudinaryDB.Where(v => v.UserName == username).ToListAsync(); 
                return Ok( video); 
           
        } 
        [HttpPost("CommentReel")]
        public async Task<IActionResult> CommentReel([FromBody] CommentDtoWithPublicid dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.publicid) || string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.CommentText))
                return BadRequest("Invalid comment data");
            var result = await _reelRepository.CommentReel(dto);
            if (result == false)
                return NotFound("Reel not found or user does not exist");
            return Ok(result);
        }
        [HttpPost("LikeReel")]
        public async Task<IActionResult> LikeReel([FromBody] SendDataReel dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Publicid))
                return BadRequest("Invalid like data");
            var result = await _reelRepository.LikeReel(dto.LikedBy, dto.Publicid);
            if (result == false)
                return NotFound("Reel not found or user does not exist");
            return Ok(result);
        }
        [HttpPost("UnLikeReel")]
        public async Task<IActionResult> UnLikeReel([FromBody] SendDataReel dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Publicid))
                return BadRequest("Invalid unlike data");
            var result = await _reelRepository.UnLikeReel( dto.LikedBy, dto.Publicid);
            if (result == false)
                return NotFound("Reel not found or user does not exist");
            return Ok(result);
        }
        [HttpGet("GetReelByPublicid/{publicid}")]
        public async Task<IActionResult> GetReelByPublicid(string publicid)
        {
            if (string.IsNullOrEmpty(publicid))
                return BadRequest("Public ID cannot be null or empty");
            var reel = await _reelRepository.GetReelByPublicid(publicid);
            if (reel == null)
                return NotFound("Reel not found");
            return Ok(reel);
        }
        [HttpGet("GetAllFive/{loggedInUser}")]
        public async Task<IActionResult> GetAllFive(string loggedInUser)
        {
            var reels = await _reelRepository.GetFiveReel(loggedInUser);  
            return Ok(reels);
        }
    }

}

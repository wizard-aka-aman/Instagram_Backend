using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Instagram.Model;
using Instagram.Model.Tables;
using Instagram.Model.DTO;

namespace Instagram.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;
        private readonly InstagramContext _context;

        public VideosController(Cloudinary cloudinary, InstagramContext context)
        {
            _cloudinary = cloudinary;
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadVideo([FromForm]  VideoDto file)
        {
            if (file == null || file.file.Length == 0)
                return BadRequest("No file uploaded");

            using var stream = file.file.OpenReadStream();
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(file.file.FileName, stream),
                Folder = "my_videos"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                return StatusCode(500, uploadResult.Error.Message);

            // Optionally, you can save the video metadata to your database
            var video = new CloudinaryDB
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString(),
                CreatedAt = DateTime.Now
            };
            _context.CloudinaryDB.Add(video);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUrl.ToString()
            });
        }

        [HttpGet("{publicId}")]
        public IActionResult GetVideoUrl(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
                return BadRequest("Public ID cannot be null or empty");
            publicId = "my_videos/" + publicId;
            var video = _context.CloudinaryDB.FirstOrDefault(v => v.PublicId == publicId);
            if (video == null)
                return NotFound("Video not found");

            return Ok(new { Url = video.Url });
        }


    }

}

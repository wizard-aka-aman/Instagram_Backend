using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.DTO
{
    public class PostsDto
    {
        [StringLength(300, ErrorMessage = "Caption can't exceed 300 characters")]
        public string? Caption { get; set; }
        public string UserName{ get; set; }
        public IFormFile imageFile { get; set; }
    }
}

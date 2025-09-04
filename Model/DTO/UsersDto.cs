using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.DTO
{
    public class UsersDto
    {
        public int UsersId { get; set; }

        [RegularExpression(@"^[A-Za-z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores.")]

        public string UserName { get; set; }


        public string Email { get; set; }


        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name can't exceed 100 characters")]
        public string FullName { get; set; }


        [StringLength(200, ErrorMessage = "Bio can't exceed 200 characters")]
        public string? Bio { get; set; }

        public string? ProfilePicture { get; set; }
        public string? Gender { get; set; }
        public int FollowersCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;
        public bool? IsPublic { get; set; }
    }
} 
 
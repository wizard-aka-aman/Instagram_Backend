using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Tables
{
    public class Users
    {
        [Key]
        public int UsersId { get; set; }


        [Required(ErrorMessage = "UserName is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "User name must be between 3 and 50 characters")]
        public string  UserName { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{6,}$",
     ErrorMessage = "Password must be at least 6 characters long and contain at least one uppercase letter, one number, and one special character")]

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string PasswordHash { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name can't exceed 100 characters")]
        public string FullName{ get; set; }

        [StringLength(200, ErrorMessage = "Bio can't exceed 200 characters")]
        public string? Bio { get; set; }

        public byte[]? ProfilePicture { get; set; }

        public string? Gender { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int FollowersCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;
        //public ICollection<string>? LikedPost { get; set; } // Navigation property to Posts table
    }
}

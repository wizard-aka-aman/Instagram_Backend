using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Tables
{
    public class Story
    {
        [Key]
        public int StoryId { get; set; }
        public string Username { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ExpirationTime { get; set; } = DateTime.Now.AddHours(24); // Default to 24 hours expiration
        
    }
}

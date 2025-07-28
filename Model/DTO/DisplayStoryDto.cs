using Instagram.Model.Tables;

namespace Instagram.Model.DTO
{
    public class DisplayStoryDto
    {
        public int StoryId { get; set; }
        public string? ImageUrl { get; set; }
        public double CreatedAt { get; set; } 
        public DateTime ExpirationTime { get; set; } = DateTime.Now.AddHours(24); // Default to 24 hours expiration
         
        public List<StorySeen>? SeenBy { get; set; } // List of users who have seen the story
        public bool IsSeen { get; set; } = false; // Default to false

    }
}

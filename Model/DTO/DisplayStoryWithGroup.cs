namespace Instagram.Model.DTO
{
    public class DisplayStoryWithGroup
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string? ProfilePicture { get; set; }
        public List<DisplayStoryDto>? DisplayStories { get; set; }
        public bool IsSeen { get; set; } = false; 
    }
}

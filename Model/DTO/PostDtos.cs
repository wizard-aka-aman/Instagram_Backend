namespace Instagram.Model.DTO
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string? Caption { get; set; }
        public string ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? Location { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime CreatedAt { get; set; }

        // Optional: agar post ke saath user info bhi dikhani ho
        public string UserName { get; set; }
    }

}

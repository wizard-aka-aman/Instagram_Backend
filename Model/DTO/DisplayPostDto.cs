namespace Instagram.Model.DTO
{
    public class DisplayPostDto
    {
        public int PostId { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }
        public List<LikeDto> Likes { get; set; }
        public List<CommentDto> Comments { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public string ProfilePicture { get; set; }
        public string UserName { get; set; }

    }
}

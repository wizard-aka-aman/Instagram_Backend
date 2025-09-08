namespace Instagram.Model.DTO
{
    public class DisplayReelDto
    {
        public int ReelId { get; set; }
        public string Publicid { get; set; }
        public string Descripton { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Url { get; set; }
        public List<ReelLikeDto> Likes { get; set; }
        public List<ReelCommentDto> Comments { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public string? ProfilePicture { get; set; }
        public string UserName { get; set; }
        public bool IsLikedLoggedInUser { get; set; } = false;
        public bool IsLoggedInUserFollow { get; set; } = false;
        public bool? IsPublic { get; set; } = false;
        public bool IsRequested { get; set; } = false;
        public bool IsSeenUserFollwingMeVariable { get; set; } = false;
        public bool AlreadyFollowing { get; set; } = false;

    }
}

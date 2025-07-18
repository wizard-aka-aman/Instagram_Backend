namespace Instagram.Model.DTO
{
    public class CommentDtoWithPostId
    {
        public string CommentText { get; set; } 
        public string UserName { get; set; } 
        public int PostId { get; set; }
    }
}

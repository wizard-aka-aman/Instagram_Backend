namespace Instagram.Model.DTO
{
    public class CommentDto
    {
        public string CommentText { get; set; }

        public DateTime CommentedAt { get; set; } = DateTime.UtcNow;
         
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
    }
}

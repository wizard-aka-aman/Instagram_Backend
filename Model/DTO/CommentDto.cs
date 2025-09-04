namespace Instagram.Model.DTO
{
    public class CommentDto
    {
        public string CommentText { get; set; }

        public DateTime CommentedAt { get; set; } = DateTime.Now;
         
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }
    }
}

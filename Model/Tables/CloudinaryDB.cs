namespace Instagram.Model.Tables
{
    public class CloudinaryDB
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; } 
        public DateTime CreatedAt { get; set; } 
        
        public string UserName { get; set; }

        public int LikeCount { get; set; } 

        public int CommentCount { get; set; }
        
        public string? ProfilePicture { set; get; }
        public string? Description { set; get; }


    }
}

using Instagram.Model.Tables;

namespace Instagram.Model.DTO
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public bool IsSeen { get; set; }
        public DateTime SeenAt { get; set; }
        public string? ProfilePicture { get; set; }
        public string UserName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string NotificationText { get; set; }
        public string LoggedInUserName { get; set; }
        public Posts? PostId { get; set; }
        public CloudinaryDB? reelId { get; set; }
    }
}

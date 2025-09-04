namespace Instagram.Model.Tables
{
    public class Notification
    {
        public int Id { get; set; }
        public bool IsSeen { get; set; }
        public DateTime? SeenAt { get; set; }
        public string? ProfilePicture { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string NotificationText { get; set; }
        public string LoggedInUserName { get; set; }
        public Posts? PostId { get; set; }
        public CloudinaryDB? reelId { get; set; }
    }
}

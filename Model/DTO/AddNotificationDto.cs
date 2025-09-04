namespace Instagram.Model.DTO
{
    public class AddNotificationDto
    {
        public string LoggedInUser { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public int? PostId { get; set; }
        public string? reelId { get; set; }
    }
}

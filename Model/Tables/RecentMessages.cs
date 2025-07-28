namespace Instagram.Model.Tables
{
    public class RecentMessages
    {
        public int RecentMessagesId { get; set; }
        public string? LastMessage { get; set; }
        public string? ProfilePicture { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string LoggedInUsername { get; set; }

        public DateTime LastMessageDateTime { get; set; }

    }
}

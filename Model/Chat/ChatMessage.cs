namespace Instagram.Model.Chat
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }

        public string? Reaction { get; set; } // Optional, used for Reaction
    }
}

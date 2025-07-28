namespace Instagram.Model.Tables
{
    public class StorySeen
    {
        public int Id { get; set; }
        public int StoryId { get; set; }
        public string SeenByUsername { get; set; }
        public DateTime SeenAt { get; set; }
        public string? ProfilePicture { get; set; }
        public Story Story { get; set; }
    }

}

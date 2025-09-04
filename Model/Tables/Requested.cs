namespace Instagram.Model.Tables
{
    public class Requested
    {
        public int Id { get; set; }
        public string? ProfilePictureOfReqFrom { get; set; }
        public string UserNameOfReqFrom { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UserNameOfReqTo { get; set; }
        public bool IsReqAccepted { get; set; } = false;
    }
}

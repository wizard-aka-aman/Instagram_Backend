using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.DTO
{
    public class DisplayUserFollower
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? ProfilePicture { get; set; }
    }
}

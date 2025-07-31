using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Tables
{
    public class ReelLike
    {
        [Key]
        public int Id { get; set; }

        public string publicId { get; set; }
        //public virtual Posts Post { get; set; }

        public string UserName { get; set; }
        public string? ProfilePicture { get; set; }
        //public virtual Users User { get; set; }

        public DateTime LikedAt { get; set; } = DateTime.Now;
    }
}

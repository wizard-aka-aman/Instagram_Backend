using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Tables
{
    public class Likes
    {
        [Key]
        public int LikeId { get; set; }

        public int PostId { get; set; } 
        //public virtual Posts Post { get; set; }

        public string UserName{ get; set; } 
        public string? ProfilePicture{ get; set; } 
        //public virtual Users User { get; set; }

        public DateTime LikedAt { get; set; } = DateTime.Now;
    }
}

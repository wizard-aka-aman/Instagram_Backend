using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Tables
{
    public class ReelComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string CommentText { get; set; }

        public DateTime CommentedAt { get; set; } = DateTime.Now;

        public string publicId { get; set; }
        //public virtual Posts Post { get; set; }

        public string UserName { get; set; }
        public string? ProfilePicture { get; set; }
        //public virtual Users User { get; set; }
    }
}

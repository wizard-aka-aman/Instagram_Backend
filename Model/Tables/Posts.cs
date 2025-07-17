using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Tables
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; } 
        [StringLength(300, ErrorMessage = "Caption can't exceed 300 characters")]
        public string? Caption { get; set; }
        // Foreign key to Users table 

        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }  
        public string? VideoUrl { get; set; }
        public string? Location { get; set; }
        public int LikesCount { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; } = null;

        public int UserId { get; set; } // foreign key to Users table
        public virtual Users? User { get; set; } // Navigation property to Users table
        //public virtual ICollection<Comments>? Comments { get; set; } // Navigation property to Comments table
        //public virtual ICollection<Likes>? Likes { get; set; } // Navigation property to Likes table 

    }
}

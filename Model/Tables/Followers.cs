using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Tables
{
    public class Followers
    {
        [Key]
        public int FollowerId { get; set; }
        public string UserName { get; set; } // The username of the follower
        public string FollowerUserName { get; set; } // The username of the user being followed
        public DateTime FollowedAt { get; set; } = DateTime.Now; // Timestamp of when the follow action occurred
    }
}

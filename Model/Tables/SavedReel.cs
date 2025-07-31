using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Tables
{
    public class SavedReel
    {
        [Key]
        public int SavedId { get; set; }

        public string UserName { get; set; }

        public CloudinaryDB CloudinaryDB{ get; set; }
        public DateTime SavedAt { get; set; }
    }
}

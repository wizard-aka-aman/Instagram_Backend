using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.Tables
{
    public class Saved
    {
        [Key]
        public int SavedId { get; set; }
        
        public string UserName { get; set; }

        public Posts Posts { get; set; }
        public DateTime SavedAt { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;

namespace Instagram.Model.DTO
{
    public class EditUserDto
    {  
         

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name can't exceed 100 characters")]
        public string FullName { get; set; }

        [StringLength(200, ErrorMessage = "Bio can't exceed 200 characters")]
        public string? Bio { get; set; }
 
        public string? Gender { get; set; }
         
    }
}

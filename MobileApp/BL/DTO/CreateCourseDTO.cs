using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class CreateCourseDTO
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 Characater")]
        [MinLength(3, ErrorMessage = "Min Length is 3 Characater")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 Characater")]
        [MinLength(3, ErrorMessage = "Min Length is 3 Characater")]
        public string Description { get; set; }
    }
}

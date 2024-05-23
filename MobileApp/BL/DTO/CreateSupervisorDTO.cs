using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class CreateSupervisorDTO
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 Characater")]
        [MinLength(3, ErrorMessage = "Min Length is 3 Characater")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Phone is Required")]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Invalid Phone Number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Email")]
        public string? Email { get; set; }
    }
}

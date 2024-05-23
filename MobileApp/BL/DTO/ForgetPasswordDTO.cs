using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class ForgetPasswordDTO
    {
        [Required(ErrorMessage ="Email is Required")]
        public string ?Email {  get; set; }
  
    }
}

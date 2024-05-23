using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class ForgetPasswordDTO
    {
        [Required(ErrorMessage ="البريد الالكترونى مطلوب ")]
        public string ?Email {  get; set; }
  
    }
}

using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage="كلمة المرور مطلوبة")]
        public string? password { get; set; }

        [Required(ErrorMessage = "البريد الالكترونى مطلوب")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "رمز التحقق مطلوب")]
        public string? token { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class UserSignUpDTO
    {
        [EmailAddress(ErrorMessage = "بريد الكترونى غير صالح")]
        [Required(ErrorMessage = "البريد الالكترونى مطلوب")]
        public string Email { get; set; }
    

        [Required(ErrorMessage = "اسم المستخدم مطلوب ")]
        [MaxLength(50, ErrorMessage = "افصي طول للاسم 50 حرف")]
        [MinLength(3, ErrorMessage = "اقل طول للاسم 3 حروف")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "رقم هاتف غير صالح")]
        public string? Phone { get; set; }
    }
}

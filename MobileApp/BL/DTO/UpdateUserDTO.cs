using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class UpdateUserDTO
    {
        [Required(ErrorMessage = "الرقم التعريفي مطلوب")]
        public string Id { get; set; }

        [Required(ErrorMessage = "اسم المستخدم مطلوب ")]
        [MaxLength(50, ErrorMessage = "افصي طول للاسم 50 حرف")]
        [MinLength(3, ErrorMessage = "اقل طول للاسم 3 حروف")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "رقم هاتف غير صالح")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "البريد الالكترونى مطلوب")]
        [DataType(DataType.EmailAddress, ErrorMessage = "بريد الكترونى غير صالح")]
        public string? Email { get; set; }
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "تاكيد كلمة المرور مطلوب")]
        [Compare("Password", ErrorMessage = "تاكيد كلمة المرور يجب ان يكون مثل كلمة السر")]
        public string ConfirmPassword { get; set; }
    }
}

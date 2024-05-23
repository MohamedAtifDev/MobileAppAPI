using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.BL.VM
{
    public class SignUpDTO
    {
        [EmailAddress(ErrorMessage = "بريد الكترونى غير صالح")]
        [Required(ErrorMessage = "البريد الالكترونى مطلوب")]
        public string Email { get; set; }
        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [DataType(DataType.Password)]
   
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "تاكيد كلمة المرور مطلوب")]
        [Compare("Password",ErrorMessage = "تاكيد كلمة المرور يجب ان يكون مثل كلمة السر")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "اسم المستخدم مطلوب ")]
        [MaxLength(50, ErrorMessage = "افصي طول للاسم 50 حرف")]
        [MinLength(3, ErrorMessage = "اقل طول للاسم 3 حروف")]
        public string userName { get; set; }
    
    }

}

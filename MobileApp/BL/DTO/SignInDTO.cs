using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class SignInDTO
    {


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string Password { get; set; }

        [Required(ErrorMessage = "البريد الالكترونى مطلوب")]
        public string Email { get; set; }

        public bool ?remember { get; set; }
    }
}

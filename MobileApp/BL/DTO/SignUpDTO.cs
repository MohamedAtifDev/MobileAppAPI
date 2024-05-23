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
        [EmailAddress(ErrorMessage = "Enter valid mail")]
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Min length is 5")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password Required")]
        [Compare("Password",ErrorMessage = "Confirm Password Must Match Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "min length is 5")]
        public string userName { get; set; }
    
    }

}

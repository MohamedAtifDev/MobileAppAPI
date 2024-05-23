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
        [Required(ErrorMessage = "please Enter Password")]
        public string Password { get; set; }

        [Required(ErrorMessage ="please Enter user name")]
        [MinLength(5, ErrorMessage = "min length is 5")]
        public string UserName { get; set; }

        public bool remember { get; set; }
    }
}

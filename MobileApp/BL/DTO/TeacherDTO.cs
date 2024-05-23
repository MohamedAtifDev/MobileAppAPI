using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class TeacherDTO
    {
        [Required(ErrorMessage ="ID is Required")]
        public  int Id {  get; set; }

        [Required(ErrorMessage = "اسم المستخدم مطلوب ")]
        [MaxLength(50, ErrorMessage = "افصي طول للاسم 50 حرف")]
        [MinLength(3, ErrorMessage = "اقل طول للاسم 3 حروف")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "رقم هاتف غير صالح")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = " العنوان  مطلوب ")]
        [MaxLength(50, ErrorMessage = "افصي طول لللعنوان 50 حرف")]
        [MinLength(3, ErrorMessage = "اقل طول للعنوان 3 حروف")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "البريد الالكترونى مطلوب")]
        [DataType(DataType.EmailAddress, ErrorMessage = "بريد الكترونى غير صالح")]
        public string? Email { get; set; }
    }
}

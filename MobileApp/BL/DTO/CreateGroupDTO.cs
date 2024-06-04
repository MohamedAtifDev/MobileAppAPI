using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class CreateGroupDTO
    {

        [Required(ErrorMessage ="اسم المجموعة مطلوب")]
        
        public string Name { get; set; }

        [Required(ErrorMessage = "سعر المجموعة مطلوب")]
        public double Price { get; set; }

        [Required(ErrorMessage = "عدد طلاب المجموعة مطلوب")]
        public int NumberOfStudents { get; set; }
    }
}

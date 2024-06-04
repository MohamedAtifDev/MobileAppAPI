using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class GroupDTO
    {
        [Required(ErrorMessage ="الرقم التعريفى مطلوب ")]
        public int Id { get; set; }
        [Required(ErrorMessage = "اسم المجموعة مطلوب")]

        public string Name { get; set; }

        [Required(ErrorMessage = "سعر المجموعة مطلوب")]
        public double Price { get; set; }

        [Required(ErrorMessage = "عدد طلاب المجموعة مطلوب")]
        public int NumberOfStudents { get; set; }
    }
}

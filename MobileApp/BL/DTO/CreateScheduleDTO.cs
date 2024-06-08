using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class CreateScheduleDTO
    {
       

        [Required(ErrorMessage = "اليوم مطلوب")]
        public string Day { get; set; }

        [Required(ErrorMessage = "الوقت مطلوب")]

        public string Time { get; set; }


        [Required(ErrorMessage = "الصف الدراسى مطلوب")]
        public int AcademicYearId { get; set; }

        [Required(ErrorMessage = "المادة مطلوبة")]
        public int CourseId { get; set; }


        [Required(ErrorMessage = "المعلم مطلوب")]

        public int TeacherId { get; set; }

        [Required(ErrorMessage = "المجموعة مطلوبة")]
        public int GroupID { get; set; }
    }
}

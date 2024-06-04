using System.ComponentModel.DataAnnotations;

namespace MobileApp.BL.DTO
{
    public class AcademicYearCoursesDTO
    {
        [Required(ErrorMessage = "الصف الدراسى مطلوب")]
        public int AcademicYearId { get; set; }


        [Required(ErrorMessage = "المادة مطلوبة")]
        public int CourseId { get; set; }

    }
}

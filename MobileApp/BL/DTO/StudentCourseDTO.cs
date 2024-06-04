using MobileApp.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.BL.DTO
{
    public class StudentCourseDTO
    {
        public StudentCourseDTO()
        {
                AssignDate= DateTime.Now;
        }
        [Required(ErrorMessage ="المادة مطلوبة")]
        public int CourseId { get; set; }


        [Required(ErrorMessage = "الطالب مطلوب")]
        public string StudentId { get; set; }









        [Required(ErrorMessage = "الصف الدراسى مطلوب")]
        public int AcademicYearId { get; set; }






        [Required(ErrorMessage = "المعلم مطلوب")]
        public int TeacherId { get; set; }


        public DateTime AssignDate { get; set; }
    }
}

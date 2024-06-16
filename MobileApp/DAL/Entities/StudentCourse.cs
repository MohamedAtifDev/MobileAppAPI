using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{
    public class StudentCourse
    {
   
        public string StudentId { get; set;}

        [ForeignKey(nameof(StudentId))]
        public AppUser Student { get; set;}

        public int AcademicYearId { get; set; }

        public int CourseId { get; set; }




        public int TeacherId { get; set; }


        public int? GroupID { get; set; }

        [ForeignKey("AcademicYearId,CourseId,TeacherId")]


        public AcademicYearCoursesTeachers AcademicYearCoursesTeachers { get; set; }


        [ForeignKey("GroupID")]
        public Group Group {  get; set; }



        public DateTime AssignDate {  get; set; }   
    }
}

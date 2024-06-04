using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{
    public class CourseMaterialLinks
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }

        public string url {  get; set; }


        public string Title {  get; set; }

        public string Description { get; set; } 
        public int AcademicYearId { get; set; }

        public int CourseId { get; set; }

  


        public int TeacherId { get; set; }


        [ForeignKey("AcademicYearId,CourseId,TeacherId")]


        public AcademicYearCoursesTeachers academicYearCoursesTreachers { get; set; }
    }
}

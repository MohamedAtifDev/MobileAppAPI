using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{
    public class Schedules
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public string Day {  get; set; }


        [DataType(DataType.Time)]
        public TimeOnly Time { get; set; }


        public int AcademicYearId { get; set; }

        public int CourseId { get; set; }




        public int TeacherId { get; set; }


        [ForeignKey("AcademicYearId,CourseId,TeacherId")]


        public AcademicYearCoursesTeachers academicYearCoursesTreachers { get; set; }

    }
}

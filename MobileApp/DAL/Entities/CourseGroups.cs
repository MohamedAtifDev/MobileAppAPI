using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MobileApp.DAL.Entities
{
    public class CourseGroups
    {

        public int AcademicYearId { get; set; }

        public int CourseId { get; set; }




        public int TeacherId { get; set; }


        [ForeignKey("AcademicYearId,CourseId,TeacherId")]


        public AcademicYearCoursesTeachers academicYearCoursesTreachers { get; set; }

        [AllowNull]
        public int? GroupId {  get; set; }

        [ForeignKey("GroupId")]
        [AllowNull]
        public Group Group { get; set; }

        public Double Price { get; set; }

        public int NumberOfStudents {  get; set; }

        //public IEnumerable<StudentCourse>studentCourses { get; set; }

        public IEnumerable<Schedules> Schedules { get; set; }
    }
}

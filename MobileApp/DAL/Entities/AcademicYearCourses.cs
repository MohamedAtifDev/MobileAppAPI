using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{

  
    public class AcademicYearCourses
    {
        public int AcademicYearId {  get; set; }

        public int CourseId {  get; set; }

        [ForeignKey(nameof(AcademicYearId))]
        public AcademicYear AcademicYear { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
        public IEnumerable<AcademicYearCoursesTeachers> AcademicYearCoursesTeachers { get; set; }


  
    }
}

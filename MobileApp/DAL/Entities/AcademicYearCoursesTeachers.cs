using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.DAL.Entities
{
    [Index("YoutubeLink", IsUnique = true)]
    public class AcademicYearCoursesTeachers
    {

        public int AcademicYearId { get; set; }

        public int CourseId { get; set; }


        [ForeignKey("AcademicYearId,CourseId")]

        public AcademicYearCourses AcademicYearCourses { get; set; }
       


        public int TeacherId { get; set; }


        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }



        public string YoutubeLink { get; set; }
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int NumberOfLessons { get; set; }

        public IEnumerable<CourseGroups> groups { get; set; }
        public IEnumerable<CourseMaterialLinks> CourseMaterialLinks { get; set; }

        public IEnumerable<CourseMaterialFiles> CourseMaterialFiles { get; set; }
        //public IEnumerable<StudentCourse> StudentCourses { get; set; }

        //public IEnumerable<Schedules> Schedules { get; set; }
    }
}

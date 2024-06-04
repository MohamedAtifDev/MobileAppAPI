using MobileApp.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.BL.DTO
{
    public class AcademicYearCoursesTeachersDTO
    {
        public int AcademicYearId { get; set; }

        public int CourseId { get; set; }




        //public AcademicYearCourses AcademicYearCourses { get; set; }



        public int TeacherId { get; set; }


        //public Teacher Teacher { get; set; }

        public string YoutubeLink { get; set; }
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int NumberOfLessons { get; set; }

        //public IEnumerable<CourseMaterialLinks> CourseMaterialLinks { get; set; }
        //public IEnumerable<StudentCourse> StudentCourses { get; set; }

        //public IEnumerable<Schedules> Schedules { get; set; }
    }
}

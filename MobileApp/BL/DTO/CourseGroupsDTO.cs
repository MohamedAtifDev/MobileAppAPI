using MobileApp.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.BL.DTO
{
    public class CourseGroupsDTO
    {
        public int AcademicYearId { get; set; }

        public int CourseId { get; set; }




        public int TeacherId { get; set; }




        public int GroupId { get; set; }

    

        public Double Price { get; set; }

        public int NumberOfStudents { get; set; }

        //public IEnumerable<StudentCourseDTO> studentCourses { get; set; }

        //public IEnumerable<ScheduleDTO> Schedules { get; set; }
    }
}

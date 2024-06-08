using MobileApp.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileApp.BL.DTO
{
    public class UpdateStudentCourse
    {
        public int old_CourseId { get; set; }
       
        public int ?New_CourseId { get; set; }
        public string StudentId { get; set; }

        public int Old_GroupId { get; set; }
        public int old_TeacherID { get; set; }
        public int Old_AcademicYear{ get; set; }
        public int ?New_TeacherID { get; set; }
        public int ?New_AcademicYear { get; set; }
        public int ?New_GroupId { get; set; }

        public DateTime AssignDate { get; set; }

    }
}

using MobileApp.BL.DTO;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface IStudentCourse
    {
        void Add(StudentCourse StudentCourse);
        void Update(StudentCourse StudentCourse);
      
        void Delete(StudentCourse studentCourse);
        IEnumerable<StudentCourse> GetAll();
      

        StudentCourse GetById(StudentCourse studentCourse);
        IEnumerable<MyCoursesDTO> GetStudentCourses(string StudentId);

        IEnumerable<MyCoursesDTO> GetStudentCoursesSchedules(string StudentId);
        int Count();
    }
}

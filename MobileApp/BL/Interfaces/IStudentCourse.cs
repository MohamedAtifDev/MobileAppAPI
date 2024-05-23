using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface IStudentCourse
    {
        void Add(StudentCourse StudentCourse);
        void Update(StudentCourse StudentCourse);
        void Delete(int CourseId,int StudentId);

        IEnumerable<StudentCourse> GetAll();
        StudentCourse GetById(int CourseId, int StudentId);

        int Count();
    }
}

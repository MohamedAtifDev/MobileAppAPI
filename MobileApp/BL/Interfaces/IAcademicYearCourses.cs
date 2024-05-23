using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface IAcademicYearCourses
    {
        void Add(AcademicYearCourses AcademicYearCourses);
        void Update(AcademicYearCourses AcademicYearCourses);
        void Delete(int AcademicYear, int CourseId);

        IEnumerable<AcademicYearCourses> GetAll();
        AcademicYearCourses GetById(int AcademicYear, int CourseI);

        int Count();
    }
}

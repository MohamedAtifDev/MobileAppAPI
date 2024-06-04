using MobileApp.BL.DTO;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface IAcademicYeatCoursesTeachers
    {
        void Add(AcademicYearCoursesTeachers academicYearCoursesTeachers);
        void Delete(int AcademicYearID, int CourseId, int TeacherId);

        void Update(UpdateDTO update);
        IEnumerable<AcademicYearCoursesTeachers> GetAll();

    AcademicYearCoursesTeachers GetById(int AcademicYearID,int CourseId,int TeacherID);
        IEnumerable<TeacherDTO> GetTeachers(int AcademicYearID, int CourseId);

    }
}

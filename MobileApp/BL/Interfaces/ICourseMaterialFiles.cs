using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface ICourseMaterialFiles
    {
        IEnumerable<CourseMaterialFiles> GetAll();

        CourseMaterialFiles GetById(int id);

        void Add(CourseMaterialFiles CourseMaterialFiles);
        IEnumerable<CourseMaterialFiles> GetSpecial(int AcademicYear, int Course, int Teacher);

        void Update(CourseMaterialFiles CourseMaterialFiles);

        void Delete(int id);
    }
}

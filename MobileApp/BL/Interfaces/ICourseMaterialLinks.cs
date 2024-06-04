using MobileApp.BL.DTO;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface ICourseMaterialLinks
    {
        IEnumerable<CourseMaterialLinks> GetAll();

       CourseMaterialLinks GetById(int id);

        void Add(CourseMaterialLinks courseMaterialLinks);
        IEnumerable<CourseMaterialLinks> GetSpecial (int AcademicYear,int Course,int Teacher);

      void Update(CourseMaterialLinks courseMaterialLinks);

        void Delete(int id);
    }
}

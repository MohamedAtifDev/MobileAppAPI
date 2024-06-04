using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface IUnAcademicCourse
    {
        void Add(UnAcademicCourse Course);
        void Update(UnAcademicCourse Course);
        void Delete(int id);

        IEnumerable<UnAcademicCourse> GetAll();
        UnAcademicCourse GetById(int id);

        int Count();
    }
}

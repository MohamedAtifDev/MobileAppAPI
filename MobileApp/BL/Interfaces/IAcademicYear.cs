using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface IAcademicYear
    {
        void Add(AcademicYear AcademicYear);
        void Update(AcademicYear AcademicYear);
        void Delete(int id);

        IEnumerable< AcademicYear> GetAll();
        AcademicYear GetById(int id);

        int Count();
    }
}

using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface ITeacher
    {

        void Add(Teacher Teacher);
        void Update(Teacher Teacher);
        void Delete(int id);

        IEnumerable<Teacher> GetAll();
        Teacher GetById(int id);

        int Count();
    }
}

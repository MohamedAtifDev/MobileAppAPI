using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface IStudent
    {
      void Add(Student student);
        void Update(Student student);
        void Delete(int id);

        IEnumerable<Student> GetAll();
        Student GetById(int id);

        int Count();
    }
}

using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface ICourse
    {
        void Add(Course Course);
        void Update(Course Course);
        void Delete(int id);

        IEnumerable<Course> GetAll();
        Course GetById(int id);

        int Count();
    }
}

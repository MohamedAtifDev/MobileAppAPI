using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface IGroup
    {
        void Add(Group Group);
        void Update(Group Group);
        void Delete(int id);

        IEnumerable<Group> GetAll();
        Group GetById(int id);

        int Count();
    }
}

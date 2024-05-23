using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface ISupervisor
    {
        void Add(Supervisor supervisor);
        void Update(Supervisor supervisor);
        void Delete(int id);

        IEnumerable<Supervisor> GetAll();
        Supervisor GetById(int id);

        int Count();
    }
}

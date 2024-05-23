using Microsoft.EntityFrameworkCore;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class SupervisorRepo:ISupervisor
    {
        private readonly DataContext db;

        public SupervisorRepo(DataContext db)
        {
            this.db = db;
        }

        public void Add(Supervisor supervisor)
        {
          db.Supervisors.Add(supervisor);
            db.SaveChanges();
        }

        public int Count()
        {
           return db.Supervisors.Count();
        }

        public void Delete(int id)
        {
            var data = db.Supervisors.Find(id);
            db.Supervisors.Remove(data);
            db.SaveChanges();
        }

        public IEnumerable<Supervisor> GetAll()
        {
           return db.Supervisors.AsNoTracking().ToList();
        }

        public Supervisor GetById(int id)
        {
            return db.Supervisors.Find(id);
        }

        public void Update(Supervisor supervisor)
        {
           db.Entry(supervisor).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}

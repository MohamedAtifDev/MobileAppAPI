using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class GroupRepo : IGroup
    {
        private readonly DataContext db;

        public GroupRepo(DataContext db)
        {
            
this.db = db;
        }
        public void Add(Group Group)
        {
            db.groups.Add(Group);
            db.SaveChanges();
        }

        public int Count()
        {
          return db.groups.Count();
        }

        public void Delete(int id)
        {
            db.groups.Remove(db.groups.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Group> GetAll()
        {
            return db.groups.ToList();
        }

        public Group GetById(int id)
        {
            return db.groups.Find(id);
        }

        public void Update(Group Group)
        {
            db.Entry(Group).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}

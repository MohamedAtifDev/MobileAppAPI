using Microsoft.EntityFrameworkCore;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class TeacherRepo:ITeacher
    {
        private readonly DataContext db;

        public TeacherRepo(DataContext db)
        {
            this.db = db;
        }

        public void Add(Teacher Teacher)
        {
           db.Teachers.Add(Teacher);
            db.SaveChanges();
        }

        public int Count()
        {
            return db.Teachers.Count();
        }

        public void Delete(int id)
        {
          var data=db.Teachers.Find(id);
            db.Teachers.Remove(data);
            db.SaveChanges();
        }

        public IEnumerable<Teacher> GetAll()
        {
            return db.Teachers.AsNoTracking().ToList();
        }

        public Teacher GetById(int id)
        {
            return db.Teachers.Find(id);
        }

        public void Update(Teacher Teacher)
        {
              db.Entry(Teacher).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
                }
    }
}

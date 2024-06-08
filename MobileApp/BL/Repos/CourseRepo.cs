using Microsoft.EntityFrameworkCore;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class CourseRepo:ICourse
    {
        private readonly DataContext db;

        public CourseRepo(DataContext db)
        {
            this.db = db;
        }

        public void Add(Course Course)
        {
            db.Courses.Add(Course);
            db.SaveChanges();

        }

        public int Count()
        {
           return db.Courses.Count();
        }

        public void Delete(int id)
        {
            var data = db.Courses.Find(id);
           db.Courses.Remove(data);
            db.SaveChanges();
        }

        public IEnumerable<Course> GetAll()
        {
            return db.Courses.AsNoTracking().ToList();
        }

        public Course GetById(int id)
        {
          return db.Courses.Find(id);
        }

        public void Update(Course Course)
        {
            db.ChangeTracker.Clear();
            db.Entry(Course).State= EntityState.Modified;
            db.SaveChanges();
        }
    }
}

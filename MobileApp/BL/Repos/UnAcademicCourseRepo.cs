using Microsoft.EntityFrameworkCore;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class UnAcademicCourseRepo:IUnAcademicCourse
    {
        private readonly DataContext db;

        public UnAcademicCourseRepo(DataContext db)
        {
            this.db = db;
        }

        public void Add(UnAcademicCourse Course)
        {
           db.unAcademicCourses.Add(Course);
            db.SaveChanges();
        }

        public int Count()
        {
            return db.unAcademicCourses.Count();
        }

        public void Delete(int id)
        {
            db.unAcademicCourses.Remove(db.unAcademicCourses.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<UnAcademicCourse> GetAll()
        {
            return db.unAcademicCourses.AsNoTracking().ToList();
        }

        public UnAcademicCourse GetById(int id)
        {
            return db.unAcademicCourses.Find(id);
        }

        public void Update(UnAcademicCourse Course)
        {
            db.ChangeTracker.Clear();
            db.Entry(Course).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}

using Microsoft.Identity.Client;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class CourseMaterialLinksRepo : ICourseMaterialLinks
    {
        private readonly DataContext db;

        public void Delete(int id)
        {
            db.CourseMaterialLinks.Remove(this.db.CourseMaterialLinks.Find(id));
            db.SaveChanges();
        }

        public void Add(CourseMaterialLinks courseMaterialLinks)
        {
            db.CourseMaterialLinks.Add(courseMaterialLinks);
            db.SaveChanges();
        }
        public CourseMaterialLinksRepo(DataContext db)
        {
            this.db = db;
        }
        public IEnumerable<CourseMaterialLinks> GetAll()
        {
            return this.db.CourseMaterialLinks.ToList();
        }

        public CourseMaterialLinks GetById(int id)
        {
            return this.db.CourseMaterialLinks.Find(id);
        }

        public IEnumerable<CourseMaterialLinks> GetSpecial(int AcademicYear, int Course, int Teacher)
        {
            return db.CourseMaterialLinks.Where(a => a.AcademicYearId == AcademicYear && a.CourseId == Course && a.TeacherId == Teacher);
        }

        public void Update(CourseMaterialLinks courseMaterialLinks)
        {
            db.ChangeTracker.Clear();
           db.Entry(courseMaterialLinks).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }


    }
}

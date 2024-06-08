using Microsoft.EntityFrameworkCore;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class CourseMaterialFilesRepo : ICourseMaterialFiles
    {
        private readonly DataContext db;

        public CourseMaterialFilesRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(CourseMaterialFiles CourseMaterialFiles)
        {
          db.CourseMaterialFiles.Add(CourseMaterialFiles);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.CourseMaterialFiles.Remove(db.CourseMaterialFiles.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<CourseMaterialFiles> GetAll()
        {
            return db.CourseMaterialFiles.AsNoTracking().ToList();
        }

        public CourseMaterialFiles GetById(int id)
        {
            return db.CourseMaterialFiles.Find(id);
        }

        public IEnumerable<CourseMaterialFiles> GetSpecial(int AcademicYear, int Course, int Teacher)
        {
            return db.CourseMaterialFiles.Where(a => a.AcademicYearId == AcademicYear && a.TeacherId == Teacher && a.CourseId == Course);
        }

        public void Update(CourseMaterialFiles CourseMaterialFiles) {

            db.ChangeTracker.Clear();
            db.Entry(CourseMaterialFiles).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}

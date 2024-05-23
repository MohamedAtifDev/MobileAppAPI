using Microsoft.EntityFrameworkCore;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class AcademicYearCoursesRepo : IAcademicYearCourses
    {
        private readonly DataContext db;

        public AcademicYearCoursesRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(AcademicYearCourses AcademicYearCourses)
        {
            this.db.AcademicYearCourses.Add(AcademicYearCourses);
            db.SaveChanges();
        }

        public int Count()
        {
            return db.AcademicYearCourses.Count
                ();
        }

        public void Delete(int AcademicYear, int CourseId)
        {
          var data= db.AcademicYearCourses.Where(a => a.CourseId == CourseId && a.AcademicYearId == AcademicYear).FirstOrDefault();
        }

        public IEnumerable<AcademicYearCourses> GetAll()
        {
            return db.AcademicYearCourses.AsNoTracking().ToList();
        }

        public AcademicYearCourses GetById(int AcademicYear, int CourseId)
        {
            return db.AcademicYearCourses.Where(a => a.CourseId == CourseId && a.AcademicYearId == AcademicYear).FirstOrDefault();
        }

        public void Update(AcademicYearCourses AcademicYearCourses)
        {
            db.Entry(AcademicYearCourses).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}

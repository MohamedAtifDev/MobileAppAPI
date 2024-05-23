using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class AcademicYearRepo : IAcademicYear
    {
        private readonly DataContext db;

        public AcademicYearRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(AcademicYear AcademicYear)
        {
            db.AcademicYears.Add(AcademicYear);
            db.SaveChanges();
        }

        public int Count()
        {
          return db.AcademicYears.Count();
        }

        public void Delete(int id)
        {
         db.AcademicYears.Remove(db.AcademicYears.Find(id));
            db.SaveChanges() ;  
        }

        public IEnumerable<AcademicYear> GetAll()
        {
           return db.AcademicYears.AsNoTracking().ToList(); 
        }

        public AcademicYear GetById(int id)
        {
            return db.AcademicYears.Find(id);
        }

        public void Update(AcademicYear AcademicYear)
        {
         db.Entry(AcademicYear).State=EntityState.Modified;
            db.SaveChanges();
        }
    }
}

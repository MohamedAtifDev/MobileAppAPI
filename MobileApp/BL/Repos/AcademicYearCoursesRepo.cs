using Microsoft.EntityFrameworkCore;
using MobileApp.BL.DTO;
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

            db.AcademicYearCourses.Remove(data);
        }

        public IEnumerable<AcademicYearCourses> GetAll()
        {
            return db.AcademicYearCourses.AsNoTracking().ToList();
        }

        public AcademicYearCourses GetById(int AcademicYear, int CourseId)
        {
            return db.AcademicYearCourses.Where(a => a.CourseId == CourseId && a.AcademicYearId == AcademicYear).FirstOrDefault();
        }

        public void Update(UpdateAcademicYearCoursesDTO update)
        {
            var record = new AcademicYearCourses
            {
                AcademicYearId = update.New_AcademicYearId,
                CourseId = update.New_CourseId
            };


            var records = new List<AcademicYearCoursesTeachers>();

            var records2 = new List<CourseMaterialLinks>();

            var relateddata = db.AcademicYearCoursesTeachers.Where(a=>a.AcademicYearId == update.Old_AcademicYearId && a.CourseId == update.Old_CourseId).AsNoTracking().AsEnumerable() ;

            foreach (var item in relateddata)
            {
                records.Add(item);
            }
            
            var dataToDelete = db.AcademicYearCourses.Where(a => a.AcademicYearId == update.Old_AcademicYearId && a.CourseId == update.Old_CourseId).FirstOrDefault();

            var CourseMaterialLinks = db.CourseMaterialLinks.Where(a => a.AcademicYearId == update.Old_AcademicYearId && a.CourseId == update.Old_CourseId).AsNoTracking().ToList();

            foreach (var item in CourseMaterialLinks)
            {
                records2.Add(item);
            }


            db.AcademicYearCourses.Remove(dataToDelete);
            db.AcademicYearCourses.Add(record);
            db.SaveChanges();

     
            foreach (var item in records)
            {
                item.CourseId =record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
             
            }
            db.AcademicYearCoursesTeachers.AddRange(records);



            foreach (var item in records2)
            {
                item.CourseId = record.CourseId;
                item.AcademicYearId = record.AcademicYearId;
                item.Id = 0;

            }

            db.CourseMaterialLinks.AddRange(CourseMaterialLinks);
            db.SaveChanges();
          


        }

       
    }
}

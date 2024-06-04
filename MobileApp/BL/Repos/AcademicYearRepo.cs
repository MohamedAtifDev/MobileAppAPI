using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using MobileApp.BL.DTO;
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


        public IEnumerable<AcademicYearDetails> GetAcademicYearWithRelatedCoursesAndTeachers(int id)
        {
            var data = db.AcademicYears.Where(a => a.Id == id).Include(a => a.AcademicYearCourses).ThenInclude(a => a.AcademicYearCoursesTeachers).ThenInclude(a => a.Teacher).Include(a => a.AcademicYearCourses).ThenInclude(a => a.Course);
            var res = data.Select(a => new AcademicYearDetails
            {
                AcademicYearId = a.Id,
                AcademicYearName = a.Name,
                Courses = a.AcademicYearCourses.Select(crs => crs.Course).Distinct().Select(crsdto => new CourseWithTeacherDTO
                {

                    Id = crsdto.Id,
                    Name = crsdto.Name,
                    Description = crsdto.Description,
                    teachers = (a.AcademicYearCourses.Where(bb => bb.AcademicYearId == a.Id && bb.CourseId == crsdto.Id).Select(ayt => ayt.AcademicYearCoursesTeachers.Select(aytd => new TeacherDTO
                    {
                        Id = aytd.TeacherId,

                        Email = aytd.Teacher.Email,
                        Name = aytd.Teacher.Name,
                        Phone = aytd.Teacher.Phone,
                        startDate = aytd.startDate,
                        endDate = aytd.endDate,
                        NumberOfLessons = aytd.NumberOfLessons,
                        YoutubeLink = aytd.YoutubeLink,

                    }))).FirstOrDefault()
                })

            });
            return res;

        }
            public IEnumerable<AcademicYearDetails> GetAllAcademicYearWithRelatedCoursesAndTeachers()
        {
            var data = db.AcademicYears.Include(a => a.AcademicYearCourses).ThenInclude(a => a.AcademicYearCoursesTeachers).ThenInclude(a => a.Teacher).Include(a => a.AcademicYearCourses).ThenInclude(a => a.Course);
            var res = data.Select(a => new AcademicYearDetails
            {
                AcademicYearId = a.Id,
                AcademicYearName = a.Name,
                Courses = a.AcademicYearCourses.Select(crs => crs.Course).Distinct().Select(crsdto => new CourseWithTeacherDTO
                {

                    Id = crsdto.Id,
                    Name = crsdto.Name,
                    Description = crsdto.Description,
                    teachers = (a.AcademicYearCourses.Where(bb => bb.AcademicYearId == a.Id && bb.CourseId == crsdto.Id).Select(ayt => ayt.AcademicYearCoursesTeachers.Select(aytd => new TeacherDTO
                    {
                        Id = aytd.TeacherId,

                        Email = aytd.Teacher.Email,
                        Name = aytd.Teacher.Name,
                        Phone = aytd.Teacher.Phone,
                        startDate = aytd.startDate,
                        endDate = aytd.endDate,
                        NumberOfLessons = aytd.NumberOfLessons,
                        YoutubeLink = aytd.YoutubeLink,

                    }))).FirstOrDefault()
                })

            });
            return res;
        }      
    }
}

using Microsoft.EntityFrameworkCore;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class StudentCoursesRepo : IStudentCourse
    {
        private readonly DataContext db;

        public StudentCoursesRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(StudentCourse StudentCourse)
        {
            db.StudentCourses.Add(StudentCourse);
            db.SaveChanges();
        }

        public int Count()
        {
          return db.StudentCourses.Count();
        }

        public void Delete(int CourseId, int StudentId)
        {
            var data=db.StudentCourses.Where(a => a.CourseId == CourseId && a.StudentId == StudentId).FirstOrDefault();
            db.StudentCourses.Remove(data);
            db.SaveChanges();
        }

        public IEnumerable<StudentCourse> GetAll()
        {
            return db.StudentCourses.AsNoTracking().ToList();   
        }

        public StudentCourse GetById(int CourseId, int StudentId)
        {
            return db.StudentCourses.Where(a => a.CourseId == CourseId && a.StudentId == StudentId).FirstOrDefault();
        }

        public void Update(StudentCourse StudentCourse)
        {
           db.Entry(StudentCourse).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}

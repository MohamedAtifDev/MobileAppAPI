using Microsoft.EntityFrameworkCore;
using MobileApp.BL.DTO;
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

        public void Delete(StudentCourse studentCourse)
        {
            var data = db.StudentCourses.Where(a => a.CourseId == studentCourse.CourseId && a.StudentId == studentCourse.StudentId && a.TeacherId == studentCourse.TeacherId && a.AcademicYearId == studentCourse.AcademicYearId).FirstOrDefault();
            db.StudentCourses.Remove(data);
            db.SaveChanges();
        }

        public IEnumerable<StudentCourse> GetAll()
        {
            return db.StudentCourses.AsNoTracking().ToList();   
        }

        public StudentCourse GetById(StudentCourse studentCourse)
        {
            return db.StudentCourses.Where(a => a.CourseId == studentCourse.CourseId && a.StudentId == studentCourse.StudentId&&a.TeacherId== studentCourse.TeacherId&&a.AcademicYearId== studentCourse.AcademicYearId).FirstOrDefault();
        }

        public void Update(StudentCourse StudentCourse)
        {
           db.Entry(StudentCourse).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<MyCoursesDTO> GetStudentCourses(string StudentId)
        {
       
            var data = db.StudentCourses.Where(a => a.StudentId == StudentId).Include(a => a.academicYearCoursesTreachers).ThenInclude(a => a.AcademicYearCourses).ThenInclude(a => a.Course).Include(a => a.academicYearCoursesTreachers).ThenInclude(a => a.Teacher).Include(a=>a.academicYearCoursesTreachers).ThenInclude(a=>a.AcademicYearCourses).ThenInclude(a=>a.AcademicYear);

            var res = data.Select(a => new MyCoursesDTO
            {
                AcademicYearName = a.academicYearCoursesTreachers.AcademicYearCourses.AcademicYear.Name,
                CourseName = a.academicYearCoursesTreachers.AcademicYearCourses.Course.Name,
                TeacherName = a.academicYearCoursesTreachers.Teacher.Name,
                Numberoflessons = a.academicYearCoursesTreachers.NumberOfLessons,
                Schedules = null
            }); ; ;
            return res;
        }

        public IEnumerable<MyCoursesDTO> GetStudentCoursesSchedules(string StudentId)
        {
            var data = db.StudentCourses.Where(a => a.StudentId == StudentId).Include(a => a.academicYearCoursesTreachers).ThenInclude(a => a.AcademicYearCourses).ThenInclude(a => a.Course).Include(a => a.academicYearCoursesTreachers).ThenInclude(a => a.Teacher).Include(a => a.academicYearCoursesTreachers).ThenInclude(a => a.AcademicYearCourses).ThenInclude(a => a.AcademicYear).Include(a=>a.academicYearCoursesTreachers).ThenInclude(a=>a.Schedules);

            var res = data.Select(a => new MyCoursesDTO
            {
                AcademicYearName = a.academicYearCoursesTreachers.AcademicYearCourses.AcademicYear.Name,
                CourseName = a.academicYearCoursesTreachers.AcademicYearCourses.Course.Name,
                TeacherName = a.academicYearCoursesTreachers.Teacher.Name,
                Numberoflessons = a.academicYearCoursesTreachers.NumberOfLessons,
               ZoomLink=a.academicYearCoursesTreachers.Teacher.ZoomLink,
                Schedules = a.academicYearCoursesTreachers.Schedules.Select(a =>new SchedulesDTO
                {
                    Day=a.Day,
                    Time=a.Time
                })

            }); ; ;
            return res;

        }
    }
}

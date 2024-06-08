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
            var data = db.StudentCourses.Where(a => a.CourseId == studentCourse.CourseId && a.StudentId == studentCourse.StudentId && a.TeacherId == studentCourse.TeacherId && a.AcademicYearId == studentCourse.AcademicYearId&&a.GroupID==studentCourse.GroupID).FirstOrDefault();
            db.StudentCourses.Remove(data);
            db.SaveChanges();
        }

        public IEnumerable<StudentCourse> GetAll()
        {
            return db.StudentCourses.AsNoTracking().ToList();   
        }

        public bool  IsExsits(StudentCourse studentCourse)
        {
            var data= db.StudentCourses.Where(a => a.CourseId == studentCourse.CourseId && a.StudentId == studentCourse.StudentId&&a.TeacherId== studentCourse.TeacherId&&a.AcademicYearId== studentCourse.AcademicYearId).FirstOrDefault();

            return data == null ? false : true;
        }

        public void Update(StudentCourse StudentCourse)
        {
           db.Entry(StudentCourse).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<MyCoursesDTO> GetStudentCourses(string StudentId)
        {

            var data = db.StudentCourses.Where(a => a.StudentId == StudentId).Include(a => a.AcademicYearCoursesTeachers).ThenInclude(a => a.AcademicYearCourses)
                .ThenInclude(a => a.Course).Include(a => a.AcademicYearCoursesTeachers).ThenInclude(a => a.AcademicYearCourses).ThenInclude(a => a.AcademicYear)
                .Include(a => a.AcademicYearCoursesTeachers).ThenInclude(a => a.Teacher).
                Include(a => a.Group);


            var res = data.Select(a => new MyCoursesDTO
            {
                AcademicYearName = a.AcademicYearCoursesTeachers.AcademicYearCourses.AcademicYear.Name,
                CourseName = a.AcademicYearCoursesTeachers.AcademicYearCourses.Course.Name,
                TeacherName = a.AcademicYearCoursesTeachers.Teacher.Name,
                Numberoflessons = a.AcademicYearCoursesTeachers.NumberOfLessons,
                ZoomLink = a.AcademicYearCoursesTeachers.Teacher.ZoomLink,
                GroupName = a.Group.Name,
                Schedules = null
            }); ; ;
            return res;
        }

        public IEnumerable<MyCoursesDTO> GetStudentCoursesSchedules(string StudentId)
        {
            var data = db.StudentCourses.Where(a => a.StudentId == StudentId).Include(a => a.AcademicYearCoursesTeachers).ThenInclude(a => a.AcademicYearCourses)
                .ThenInclude(a => a.Course).Include(a => a.AcademicYearCoursesTeachers).ThenInclude(a => a.AcademicYearCourses).ThenInclude(a => a.AcademicYear)
                .Include(a => a.AcademicYearCoursesTeachers).ThenInclude(a => a.Teacher).
                Include(a => a.Group).Include(a=>a.AcademicYearCoursesTeachers).ThenInclude(a=>a.groups).ThenInclude(a=>a.Schedules);


            var res = data.Select(a => new MyCoursesDTO
            {
                AcademicYearName = a.AcademicYearCoursesTeachers.AcademicYearCourses.AcademicYear.Name,
                CourseName = a.AcademicYearCoursesTeachers.AcademicYearCourses.Course.Name,
                TeacherName = a.AcademicYearCoursesTeachers.Teacher.Name,
                Numberoflessons = a.AcademicYearCoursesTeachers.NumberOfLessons,
                ZoomLink = a.AcademicYearCoursesTeachers.Teacher.ZoomLink,
                GroupName = a.Group.Name,
                Schedules = a.AcademicYearCoursesTeachers.groups.Where(grp=>grp.Group.Id==a.Group.Id && grp.AcademicYearId==a.AcademicYearId && grp.CourseId==a.CourseId&& grp.TeacherId == a.TeacherId)
                .Select(a=>a.Schedules.Select(sch=>new ScheduleDTO
                {Day=sch.Day,
                Time=sch.Time,

                })
                ).FirstOrDefault()
            }); ; ;
            return res;

        }
    }
}

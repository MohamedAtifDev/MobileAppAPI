using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Repos
{
    public class ScheduleRepo : ISchedules
    {
        private readonly DataContext db;

        public ScheduleRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(Schedules schedule)
        {
            db.schedules.Add(schedule);
            db.SaveChanges();
        }

        public IEnumerable<Schedules> GetAll()
        {
            return db.schedules.ToList();
        }

        public Schedules GetByID(int id)
        {
            return db.schedules.Find(id);
        }

        public IEnumerable<Schedules> GetCourseSchedules(int AcademicYear, int Course, int teacher,int Group)
        {
            return db.schedules.Where(A => A.AcademicYearId == AcademicYear && A.CourseId == Course && A.TeacherId == teacher && A.GroupID == Group).ToList();
        }

        public void Remove(int id)
        {
            db.Remove(db.schedules.Find(id));
            db.SaveChanges();
        }

        public void Update(Schedules schedule)
        {
            db.ChangeTracker.Clear();
            db.Entry(schedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }

        public bool IsExist(Schedules schedule)
        {
            var data=db.schedules.Where(A => A.AcademicYearId == schedule.AcademicYearId && A.CourseId == schedule.CourseId && A.TeacherId == schedule.TeacherId && A.GroupID==schedule.GroupID).ToList();
            return data.Any(a=>a.Day==schedule.Day &&a.Time==schedule.Time);
        } 
    }
}

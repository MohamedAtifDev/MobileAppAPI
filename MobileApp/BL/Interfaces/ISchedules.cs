using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface ISchedules
    {
        IEnumerable<Schedules> GetAll();

        Schedules GetByID(int id);

        IEnumerable<Schedules> GetCourseSchedules(int AcademicYear, int Course, int teacher, int Group);

        void Add(Schedules schedule);
        void Remove(int id);

        void Update(Schedules schedule);
        bool IsExist(Schedules schedule);

    }
}

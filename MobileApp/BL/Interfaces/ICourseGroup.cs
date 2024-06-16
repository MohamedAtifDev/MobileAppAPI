using MobileApp.BL.DTO;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.Interfaces
{
    public interface ICourseGroup
    {
        IEnumerable<CourseGroups> GetAll();

        void Create(CourseGroups courseGroups);

        public void update(UpdateCourseGroupDTO courseGroups);

        void Delete(CourseGroups courseGroups);

        IEnumerable<GetGroupsDTO> GetGroups(int AcademicYear,int Course,int Teacher );

        public bool isExist(int AcademicYear, int Course, int Teacher, int Group);
    }
}

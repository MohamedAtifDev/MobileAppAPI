using AutoMapper;
using MobileApp.BL.DTO;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.AutoMapper
{
    public class MyProfile:Profile
    {
        public MyProfile()
        {
            CreateMap<StudentDTO, Student>().ReverseMap();
            CreateMap<CreateStudentDTO, Teacher>().ReverseMap();
            CreateMap<TeacherDTO, Teacher>().ReverseMap();
            CreateMap<CreateTeacherDTO, Teacher>().ReverseMap();
            CreateMap<SupervisorDTO, Supervisor>().ReverseMap();
            CreateMap<CreateSupervisorDTO, Supervisor>().ReverseMap();
            CreateMap<CourseDTo, Course>().ReverseMap();
            CreateMap<CreateCourseDTO, Course>().ReverseMap();

        }
    }
}

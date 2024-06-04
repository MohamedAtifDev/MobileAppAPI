using AutoMapper;
using MobileApp.BL.DTO;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.AutoMapper
{
    public class MyProfile:Profile
    {
        public MyProfile()
        {
       
            CreateMap<CreateStudentDTO, Teacher>().ReverseMap();
            CreateMap<TeacherDTO, Teacher>().ReverseMap();
            CreateMap<CreateTeacherDTO, Teacher>().ReverseMap();
            CreateMap<AcademicYearDTO, AcademicYear>().ReverseMap();
            CreateMap<CreateAcademicYearDTO, AcademicYear>().ReverseMap();
            CreateMap<UnAcademicCourse, UnAcademicCourseDTO>().ReverseMap();
            CreateMap<CreateUnAcademicCourseDTO, UnAcademicCourse>().ReverseMap();
            CreateMap<CourseDTo, Course>().ReverseMap();
            CreateMap<CreateCourseDTO, Course>().ReverseMap();
            CreateMap<CreateGroupDTO, Group>().ReverseMap();
            CreateMap<GroupDTO,Group>().ReverseMap();
            CreateMap<StudentCourseDTO, StudentCourse>().ReverseMap();
            CreateMap<AcademicYearCourses, AcademicYearCoursesDTO>().ReverseMap();
            CreateMap<UpdateAcademicYearCoursesDTO, AcademicYearCourses>().ReverseMap();
            CreateMap<AcademicYearCoursesTeachers, AcademicYearCoursesTeachersDTO>().ReverseMap();
            CreateMap<CreateCourseMaterialLinksDTO, CourseMaterialLinks>().ReverseMap();
            CreateMap<CourseMaterialLinksDTO, CourseMaterialLinks>().ReverseMap();
        }
    }
}

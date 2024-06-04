using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileApp.BL.CustomReponse;
using MobileApp.BL.DTO;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.Entities;

namespace MobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearCoursesTeacherController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAcademicYeatCoursesTeachers academicYeatCoursesTeachers;

        public AcademicYearCoursesTeacherController(IMapper mapper,IAcademicYeatCoursesTeachers academicYeatCoursesTeachers)
        {
            this.mapper = mapper;
            this.academicYeatCoursesTeachers = academicYeatCoursesTeachers;
        }

        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<AcademicYearCoursesTeachersDTO>> GetAll()
        {
            var data = academicYeatCoursesTeachers.GetAll();
            var result = mapper.Map<IEnumerable<AcademicYearCoursesTeachersDTO>>(data);
            var message = new List<string>();
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<AcademicYearCoursesTeachersDTO>> { StatusCode = 200, Data = result, Message = message };
        }

        [HttpGet]
        [Route("GetById/{AcademicYearId}/{Courseid}/{TeacherId}")]
        public CustomReponse<AcademicYearCoursesTeachersDTO> GetById(int AcademicYearId, int Courseid,int TeacherId)
        {
            var data = academicYeatCoursesTeachers.GetById(AcademicYearId, Courseid,TeacherId);
            if (data is not null)
            {
                var result = mapper.Map<AcademicYearCoursesTeachersDTO>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<AcademicYearCoursesTeachersDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("البيانات غير صحيحة");
            return new CustomReponse<AcademicYearCoursesTeachersDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<AcademicYearCoursesTeachersDTO> Create([FromBody] AcademicYearCoursesTeachersDTO DTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var record = academicYeatCoursesTeachers.GetById(DTO.AcademicYearId,DTO.CourseId,DTO.TeacherId);
                    

                        if (record is null)
                        {
                            var data = mapper.Map<AcademicYearCoursesTeachers>(DTO);
                        academicYeatCoursesTeachers.Add(data);
                            var message = new List<string>();
                            message.Add("تم اضافة البيانات بنجاح");
                            return new CustomReponse<AcademicYearCoursesTeachersDTO> { StatusCode = 200, Data = DTO, Message = message };

                        }
                        else
                        {
                            var message = new List<string>();
                            message.Add("البيانات موجودة بالفعل");
                        }
                    

                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<AcademicYearCoursesTeachersDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            //catch (UniqueConstraintException e)
            //{
            //    var message = new List<string>();
            //    foreach (var academicYear in e.ConstraintProperties)
            //    {
            //        message.Add($"  موجودة بالفعل {academicYear} المجموعة ");
            //    }
            //    return new CustomReponse<StudentCourseDTO> { StatusCode = 400, Data = null, Message = message };

            //}
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<AcademicYearCoursesTeachersDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<UpdateDTO> Update(UpdateDTO Update)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Update.NewTeacherId is null)
                    {
                        Update.NewTeacherId = Update.TeacherId;
                        academicYeatCoursesTeachers.Update(Update);
                        var message = new List<string>();
                        message.Add("تم تعديل البيانات بنجاح ");
                        return new CustomReponse<UpdateDTO> { StatusCode = 200, Data = null, Message = message };
                    }
                    else
                    {
                        var entity = academicYeatCoursesTeachers.GetById(Update.AcademicYearId, Update.CourseId, (int)Update.NewTeacherId);
                        if (entity is null)
                        {

                            academicYeatCoursesTeachers.Update(Update);
                            var message = new List<string>();
                            message.Add("تم تعديل البيانات بنجاح ");
                            return new CustomReponse<UpdateDTO> { StatusCode = 200, Data = null, Message = message };
                        }
                        else
                        {
                            //if (Update.TeacherId == Update.NewTeacherId)
                            //{

                            //    academicYeatCoursesTeachers.Update(Update);
                            //    var message = new List<string>();
                            //    message.Add("تم تعديل البيانات بنجاح ");
                            //    return new CustomReponse<UpdateDTO> { StatusCode = 200, Data = null, Message = message };
                            //}
                            var AlreadyExist = new List<string>();
                            AlreadyExist.Add("بيانات  مسجلة بالفعل ");
                            return new CustomReponse<UpdateDTO> { StatusCode = 404, Data = null, Message = AlreadyExist };
                        }
                    }
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<UpdateDTO> { StatusCode = 400, Data = null, Message = errors };


            }

            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<UpdateDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{AcademicYearId}/{Courseid}/{TeacherId}")]
        public CustomReponse<AcademicYearCoursesTeachersDTO> Delete(int AcademicYearId, int Courseid,int TeacherId)
        {
            var data = academicYeatCoursesTeachers.GetById(AcademicYearId, Courseid,TeacherId);
            if (data is not null)
            {
                academicYeatCoursesTeachers.Delete(AcademicYearId, Courseid,TeacherId);
                var result = mapper.Map<AcademicYearCoursesTeachersDTO>(data);
                var message = new List<string>();
                message.Add("تم حذف السجل بنجاح");
                return new CustomReponse<AcademicYearCoursesTeachersDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("بيانات غير مسجلة");
            return new CustomReponse<AcademicYearCoursesTeachersDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
        }


        [HttpGet]
        [Route("GetCourseTeachers/{AcademicYearId}/{CourseId}")]
        public CustomReponse<IEnumerable<TeacherDTO>> Count(int AcademicYearId,int CourseId)
        {
            var data = academicYeatCoursesTeachers.GetTeachers(AcademicYearId,CourseId) ;

            var message = new List<string>();
            message.Add($"  تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<TeacherDTO>> { StatusCode = 200, Data = data, Message = message };

        }
    }
}

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
    public class AcademicYearCoursesController : ControllerBase
    {
        private readonly IAcademicYear academicYear;
        private readonly ICourse course;
        private readonly IMapper mapper;
        private readonly IAcademicYearCourses academicYearCourses;

        public AcademicYearCoursesController(IAcademicYear academicYear,ICourse course,IMapper mapper,IAcademicYearCourses _academicYearCourses)
        {
            this.academicYear = academicYear;
            this.course = course;
            this.mapper = mapper;
            academicYearCourses = _academicYearCourses;
        }



        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<AcademicYearCoursesDTO>> GetAll()
        {
            var data = academicYearCourses.GetAll();
            var result = mapper.Map<IEnumerable<AcademicYearCoursesDTO>>(data);
            var message = new List<string>();
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<AcademicYearCoursesDTO>> { StatusCode = 200, Data = result, Message = message };
        }

        [HttpGet]
        [Route("GetById/{AcademicYearId}/{Courseid}")]
        public CustomReponse<AcademicYearCoursesDTO> GetById(int AcademicYearId, int Courseid)
        {
            var data = academicYearCourses.GetById(AcademicYearId, Courseid);
            if (data is not null)
            {
                var result = mapper.Map<AcademicYearCoursesDTO>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<AcademicYearCoursesDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("البيانات غير صحيحة");
            return new CustomReponse<AcademicYearCoursesDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<AcademicYearCoursesDTO> Create([FromBody] AcademicYearCoursesDTO AcademicYearCoursesDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var acadmicyear = academicYear.GetById(AcademicYearCoursesDTO.AcademicYearId);
                    var coursedata = course.GetById(AcademicYearCoursesDTO.CourseId);
                    if (acadmicyear is null)
                    {
                        var message = new List<string>();
                        message.Add("صف دراسى غير موجود");
                        return new CustomReponse<AcademicYearCoursesDTO> { StatusCode = 404, Data = null, Message = message };

                    }else if(coursedata is null)
                    {
                        var message = new List<string>();
                        message.Add("مادة غير موجودة");
                        return new CustomReponse<AcademicYearCoursesDTO> { StatusCode = 404, Data = null, Message = message };


                    }
                    else {

                        var record = academicYearCourses.GetById(AcademicYearCoursesDTO.AcademicYearId, AcademicYearCoursesDTO.CourseId);
                        if (record is null)
                        {
                            var data = mapper.Map<AcademicYearCourses>(AcademicYearCoursesDTO);
                            academicYearCourses.Add(data);
                            var message = new List<string>();
                            message.Add("تم اضافة البيانات بنجاح");
                            return new CustomReponse<AcademicYearCoursesDTO> { StatusCode = 200, Data = AcademicYearCoursesDTO, Message = message };

                        }
                        else
                        {
                            var message = new List<string>();
                            message.Add("البيانات موجودة بالفعل");
                        }
                    }
                  
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<AcademicYearCoursesDTO> { StatusCode = 400, Data = null, Message = errors };


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
                return new CustomReponse<AcademicYearCoursesDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<UpdateAcademicYearCoursesDTO> Update(UpdateAcademicYearCoursesDTO Update)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = academicYearCourses.GetById(Update.New_AcademicYearId, Update.New_CourseId);
                    if (entity is null)
                    {

                        academicYearCourses.Update(Update);
                        var message = new List<string>();
                        message.Add("تم تعديل البيانات بنجاح ");
                        return new CustomReponse<UpdateAcademicYearCoursesDTO> { StatusCode = 200, Data = null, Message = message };
                    }
                    else
                    {
                        var AlreadyExist = new List<string>();
                        AlreadyExist.Add("بيانات غير مسجلة");
                        return new CustomReponse<UpdateAcademicYearCoursesDTO> { StatusCode = 404, Data = null, Message = AlreadyExist };
                    }

                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<UpdateAcademicYearCoursesDTO> { StatusCode = 400, Data = null, Message = errors };


            }

            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<UpdateAcademicYearCoursesDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{AcademicYearId}/{Courseid}")]
        public CustomReponse<AcademicYearCoursesDTO> Delete(int AcademicYearId, int Courseid)
        {
            var data = academicYearCourses.GetById(AcademicYearId, Courseid);
            if (data is not null)
            {
                academicYearCourses.Delete(AcademicYearId, Courseid);
                var result = mapper.Map<AcademicYearCoursesDTO>(data);
                var message = new List<string>();
                message.Add("تم حذف السجل بنجاح");
                return new CustomReponse<AcademicYearCoursesDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("بيانات غير مسجلة");
            return new CustomReponse<AcademicYearCoursesDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
        }


        [HttpGet]
        [Route("Count")]
        public CustomReponse<int> Count()
        {
            var data = academicYearCourses.Count(); ;

            var message = new List<string>();
            message.Add($"  سجلات {data} يوجد  ");
            return new CustomReponse<int> { StatusCode = 200, Data = data, Message = message };

        }
    }
}

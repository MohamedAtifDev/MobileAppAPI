using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileApp.BL.CustomReponse;
using MobileApp.BL.DTO;
using MobileApp.BL.Interfaces;
using MobileApp.BL.services;
using MobileApp.DAL.Entities;

namespace MobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnAcademicCourseController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnAcademicCourse unAcademicCourse;

        public UnAcademicCourseController(IMapper mapper,IUnAcademicCourse unAcademicCourse)
        {
            this.mapper = mapper;
            this.unAcademicCourse = unAcademicCourse;
        }

        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<UnAcademicCourseDTO>> GetAll()
        {
            var data = unAcademicCourse.GetAll();
            var result = mapper.Map<IEnumerable<UnAcademicCourseDTO>>(data);
            var message = new List<string>();
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<UnAcademicCourseDTO>> { StatusCode = 200, Data = result, Message = message };
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomReponse<UnAcademicCourseDTO> GetById(int id)
        {
            var data = unAcademicCourse.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<UnAcademicCourseDTO>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<UnAcademicCourseDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("Student Not Found");
            return new CustomReponse<UnAcademicCourseDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<CreateUnAcademicCourseDTO> Create([FromBody] CreateUnAcademicCourseDTO course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<UnAcademicCourse>(course);
                    var imgname = fileUploader.upload("Images", course.Img);
                    data.ImgName = imgname;
                    unAcademicCourse.Add(data);
                    var message = new List<string>();
                    message.Add("تم ااضافة المادة بنجاح");
                    return new CustomReponse<CreateUnAcademicCourseDTO> { StatusCode = 200, Data = course, Message = message };
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CreateUnAcademicCourseDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var coursedata in e.ConstraintProperties)
                {
                    message.Add($"  موجودة بالفعل {coursedata} المادة ");
                }
                return new CustomReponse<CreateUnAcademicCourseDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CreateUnAcademicCourseDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<UnAcademicCourseDTO> Update(UnAcademicCourseDTO Course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = unAcademicCourse.GetById(Course.Id);
                    if (entity is not null)
                    {
                        var data = mapper.Map<UnAcademicCourse>(Course);
                        var imgname = fileUploader.upload("Images", Course.Img);
                        data.ImgName = imgname;
                        unAcademicCourse.Update(data);
                        var message = new List<string>();
                        message.Add("تم تعديل المادة بنجاح");
                        return new CustomReponse<UnAcademicCourseDTO> { StatusCode = 200, Data = Course, Message = message };
                    }
                    else
                    {
                        var NotFoundmessage = new List<string>();
                        NotFoundmessage.Add("المادة غير موجودة");
                        return new CustomReponse<UnAcademicCourseDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
                    }

                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<UnAcademicCourseDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var course in e.ConstraintProperties)
                {
                    message.Add($"  موجودة بالفعل {course} المادة ");
                }
                return new CustomReponse<UnAcademicCourseDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<UnAcademicCourseDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomReponse<UnAcademicCourseDTO> Delete(int id)
        {
            var data = unAcademicCourse.GetById(id);
            if (data is not null)
            {
                unAcademicCourse.Delete(id);
                var result = mapper.Map<UnAcademicCourseDTO>(data);
                fileUploader.delete(data.ImgName, "Images");
                var message = new List<string>();
                message.Add("تم حذف المادة بنجاح");
                return new CustomReponse<UnAcademicCourseDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("المادة غير موجودة");
            return new CustomReponse<UnAcademicCourseDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
        }


        [HttpGet]
        [Route("Count")]
        public CustomReponse<int> Count()
        {
            var data = unAcademicCourse.Count(); ;

            var message = new List<string>();
            message.Add($"  مواد {data} يوجد  ");
            return new CustomReponse<int> { StatusCode = 200, Data = data, Message = message };

        }
    }
}

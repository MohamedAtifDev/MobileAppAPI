using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileApp.BL.CustomReponse;
using MobileApp.BL.DTO;
using MobileApp.BL.Interfaces;
using MobileApp.BL.services;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourse icourse;
        private readonly IMapper mapper;
        //private readonly DataContext db;

        public CourseController(ICourse icoures, IMapper mapper)//, DataContext db)
        {
            this.icourse = icoures;
            this.mapper = mapper;
           // this.db = db;
        }

        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<CourseDTo>> GetAll()
        {
            var data = icourse.GetAll();
            var result = mapper.Map<IEnumerable<CourseDTo>>(data);
            var message = new List<string>();
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<CourseDTo>> { StatusCode = 200, Data = result, Message = message };


        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomReponse<CourseDTo> GetById(int id)
        {
            var data = icourse.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<CourseDTo>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<CourseDTo> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("المادة غير موجودة");
            return new CustomReponse<CourseDTo> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<CourseDTo> Create([FromForm] CreateCourseDTO CreateCourseDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Course>(CreateCourseDTO);
                    var imgname = fileUploader.upload("Files", CreateCourseDTO.Img);
                    data.ImgName = imgname;
                   
                    icourse.Add(data);
                    var res = mapper.Map<CourseDTo>(data);
                    var message = new List<string>();
                    message.Add("تم ااضافة المادة بنجاح");
                    return new CustomReponse<CourseDTo> { StatusCode = 200, Data = res, Message = message };
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CourseDTo> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var course in e.ConstraintProperties)
                {
                    message.Add($"المادة  موجودة بالفعل ");
                }
                return new CustomReponse<CourseDTo> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CourseDTo> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<CourseDTo> Update([FromForm] UpdateCourseDTO CourseDTo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = icourse.GetById(CourseDTo.Id);
                    if (entity is not null)
                    {
                        var data = mapper.Map<Course>(CourseDTo);
                        var imgname = fileUploader.upload("Files", CourseDTo.Img);
                        data.ImgName = imgname;
                        
                        icourse.Update(data);
                        var res = mapper.Map<CourseDTo>(data);
                        var message = new List<string>();
                        message.Add("تم تعديل المادة بنجاح");
                        return new CustomReponse<CourseDTo> { StatusCode = 200, Data = res, Message = message };
                    }
                    else
                    {
                        var NotFoundmessage = new List<string>();
                        NotFoundmessage.Add("المادة غير موجودة");
                        return new CustomReponse<CourseDTo> { StatusCode = 404, Data = null, Message = NotFoundmessage };
                    }

                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CourseDTo> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var Course in e.ConstraintProperties)
                {
                    message.Add($"المادة  موجودة بالفعل ");
                }
                return new CustomReponse<CourseDTo> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CourseDTo> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomReponse<CourseDTo> Delete(int id)
        {
            var data = icourse.GetById(id);
            if (data is not null)
            {
                icourse.Delete(id);
                var result = mapper.Map<CourseDTo>(data);
                fileUploader.delete(data.ImgName, "Files");
                var message = new List<string>();
                message.Add("تم حذف المادة بنجاح");
                return new CustomReponse<CourseDTo> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("المادة غير موجودة");
            return new CustomReponse<CourseDTo> { StatusCode = 404, Data = null, Message = NotFoundmessage };
        }


        [HttpGet]
        [Route("Count")]
        public CustomReponse<int> Count()
        {
            var data = icourse.Count(); ;

            var message = new List<string>();
            message.Add($"  مواد {data} يوجد  ");
            return new CustomReponse<int> { StatusCode = 200, Data = data, Message = message };

        }

    

    }
}

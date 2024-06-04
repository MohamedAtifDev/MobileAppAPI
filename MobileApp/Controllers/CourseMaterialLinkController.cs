using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileApp.BL.CustomReponse;
using MobileApp.BL.DTO;
using MobileApp.BL.Interfaces;
using MobileApp.BL.services;
using MobileApp.DAL.Entities;
using System.Collections.Generic;

namespace MobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseMaterialLinkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICourseMaterialLinks courseMaterialLinks;

        public CourseMaterialLinkController(IMapper mapper, ICourseMaterialLinks courseMaterialLinks)
        {
            this.mapper = mapper;
            this.courseMaterialLinks = courseMaterialLinks;
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<CourseMaterialLinksDTO>> GetAll()
        {
            var data = courseMaterialLinks.GetAll();
            var result = mapper.Map<IEnumerable<CourseMaterialLinksDTO>>(data);
            var message = new List<string>();
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<CourseMaterialLinksDTO>> { StatusCode = 200, Data = result, Message = message };


        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomReponse<CourseMaterialLinksDTO> GetById(int id)
        {
            var data = courseMaterialLinks.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<CourseMaterialLinksDTO>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<CourseMaterialLinksDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("المعلم غير موجود");
            return new CustomReponse<CourseMaterialLinksDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }


        [HttpGet]
        [Route("GetLinks/{AcademicYearid}/{CourseId}/{TeacherId}")]
        public CustomReponse<IEnumerable<CourseMaterialLinksDTO>> GetLinks(int AcademicYearid,int CourseId,int TeacherId)
        {
            var data = courseMaterialLinks.GetSpecial(AcademicYearid,  CourseId,  TeacherId);
            if (data is not null)
            {
                var result = mapper.Map< IEnumerable<CourseMaterialLinksDTO>> (data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<IEnumerable <CourseMaterialLinksDTO >> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add(" لا يوجد فيديوهات");
            return new CustomReponse<IEnumerable<CourseMaterialLinksDTO>> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<CreateCourseMaterialLinksDTO> Create([FromBody] CreateCourseMaterialLinksDTO CourseMaterialLinksDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<CourseMaterialLinks>(CourseMaterialLinksDTO);

                    courseMaterialLinks.Add(data);
                    var message = new List<string>();
                    message.Add("تم اضافة الفيديو بنجاح");
                    return new CustomReponse<CreateCourseMaterialLinksDTO> { StatusCode = 200, Data = CourseMaterialLinksDTO, Message = message };
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CreateCourseMaterialLinksDTO> { StatusCode = 400, Data = null, Message = errors };


            }
          
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CreateCourseMaterialLinksDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<CourseMaterialLinksDTO> Update(CourseMaterialLinksDTO courseMaterialLinksDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = courseMaterialLinks.GetById(courseMaterialLinksDTO.Id);
                    if (entity is not null)
                    {
                        var data = mapper.Map<CourseMaterialLinks>(courseMaterialLinksDTO);

                        courseMaterialLinks.Update(data);
                        var message = new List<string>();
                        message.Add("تم تعديل الفيديو بنجاح");
                        return new CustomReponse<CourseMaterialLinksDTO> { StatusCode = 200, Data = courseMaterialLinksDTO, Message = message };
                    }
                    else
                    {
                        var NotFoundmessage = new List<string>();
                        NotFoundmessage.Add("الفيديو غير موجود");
                        return new CustomReponse<CourseMaterialLinksDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
                    }

                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CourseMaterialLinksDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var Teacher in e.ConstraintProperties)
                {
                    message.Add($"  موجود بالفعل{Teacher} ");
                }
                return new CustomReponse<CourseMaterialLinksDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CourseMaterialLinksDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomReponse<CourseMaterialLinksDTO> Delete(int id)
        {
            var data = courseMaterialLinks.GetById(id);
            if (data is not null)
            {

                courseMaterialLinks.Delete(id);
                var result = mapper.Map<CourseMaterialLinksDTO>(data);
                var message = new List<string>();
                message.Add("تم حذف الفيديو  بنجاح");
                return new CustomReponse<CourseMaterialLinksDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("الفيديو غير موجود");
            return new CustomReponse<CourseMaterialLinksDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }
    }
}

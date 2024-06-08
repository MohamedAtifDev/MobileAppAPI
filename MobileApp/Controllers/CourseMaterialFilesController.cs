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
    public class CourseMaterialFilesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICourseMaterialFiles courseMaterialFiles;

        public CourseMaterialFilesController(IMapper mapper, ICourseMaterialFiles courseMaterialFiles)
        {
            this.mapper = mapper;
            this.courseMaterialFiles = courseMaterialFiles;
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<CourseMaterialFilesDTO>> GetAll()
        {
            var data = courseMaterialFiles.GetAll();
            var result = mapper.Map<IEnumerable<CourseMaterialFilesDTO>>(data);
            var message = new List<string>();
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<CourseMaterialFilesDTO>> { StatusCode = 200, Data = result, Message = message };


        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomReponse<CourseMaterialFilesDTO> GetById(int id)
        {
            var data = courseMaterialFiles.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<CourseMaterialFilesDTO>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("الملف غير موجود");
            return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }


        [HttpGet]
        [Route("GetFiles/{AcademicYearid}/{CourseId}/{TeacherId}")]
        public CustomReponse<IEnumerable<CourseMaterialFilesDTO>> GetFiles(int AcademicYearid, int CourseId, int TeacherId)
        {
            var data = courseMaterialFiles.GetSpecial(AcademicYearid, CourseId, TeacherId);
            if (data.Any())
            {
                var result = mapper.Map<IEnumerable<CourseMaterialFilesDTO>>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<IEnumerable<CourseMaterialFilesDTO>> { StatusCode = 200, Data = result, Message = message };
            }
            else
            {
                var NotFoundmessage = new List<string>();
                NotFoundmessage.Add(" لا يوجد ملفات");
                return new CustomReponse<IEnumerable<CourseMaterialFilesDTO>> { StatusCode = 404, Data = null, Message = NotFoundmessage };

            }

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<CourseMaterialFilesDTO> Create([FromForm]CreateCourseMaterialFilesDTO CreateCourseMaterialFilesDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var filename=fileUploader.upload("Files", CreateCourseMaterialFilesDTO.File);
                    var data = mapper.Map<CourseMaterialFiles>(CreateCourseMaterialFilesDTO);
                    data.FileName = filename;
                   
                    courseMaterialFiles.Add(data);
                    var res=mapper.Map<CourseMaterialFilesDTO>(data);
                    var message = new List<string>();
                    message.Add("تم اضافة الملف بنجاح");
                    return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 200, Data = res, Message = message };
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 400, Data = null, Message = errors };


            }

            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<CourseMaterialFilesDTO> Update([FromForm]UpdateCourseMaterialFiles CourseMaterialFilesDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = courseMaterialFiles.GetById(CourseMaterialFilesDTO.Id);
                    if (entity is not null)
                    {
                        var data = mapper.Map<CourseMaterialFiles>(CourseMaterialFilesDTO);
                        var filename = fileUploader.upload("Files", CourseMaterialFilesDTO.File);
                        data.FileName = filename;
                      
                        courseMaterialFiles.Update(data);
                        var res = mapper.Map<CourseMaterialFilesDTO>(data);
                        var message = new List<string>();
                        message.Add("تم تعديل الملف بنجاح");
                        return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 200, Data = res, Message = message };
                    }
                    else
                    {
                        var NotFoundmessage = new List<string>();
                        NotFoundmessage.Add("الملف غير موجود");
                        return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
                    }

                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomReponse<CourseMaterialFilesDTO> Delete(int id)
        {
            var data = courseMaterialFiles.GetById(id);
            if (data is not null)
            {

                courseMaterialFiles.Delete(id);
                var result = mapper.Map<CourseMaterialFilesDTO>(data);
                var message = new List<string>();
                message.Add("تم حذف الملف  بنجاح");
                return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("الملف غير موجود");
            return new CustomReponse<CourseMaterialFilesDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }
    }
}

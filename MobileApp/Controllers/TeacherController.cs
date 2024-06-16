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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacher iteacher;
        private readonly IMapper mapper;
        //private readonly DataContext db;

        public TeacherController(ITeacher iteacher, IMapper mapper)//, DataContext db)
        {
            this.iteacher = iteacher;
            this.mapper = mapper;
            //this.db = db;
        }

        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<TeacherDTO>> GetAll()
        {
            var data = iteacher.GetAll();
            var result = mapper.Map<IEnumerable<TeacherDTO>>(data);
            var message = new List<string>();
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<TeacherDTO>> { StatusCode = 200, Data = result, Message = message };


        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomReponse<TeacherDTO> GetById(int id)
        {
            var data = iteacher.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<TeacherDTO>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<TeacherDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("المعلم غير موجود");
            return new CustomReponse<TeacherDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<CreateTeacherDTO> Create([FromForm] CreateTeacherDTO TeacherDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Teacher>(TeacherDTO);
                    var imgname = fileUploader.upload("Files", TeacherDTO.Img);
                    data.ImgName = imgname;
                  
                    iteacher.Add(data);
                    var message = new List<string>();
                    message.Add("تم اضافة المعلم بنجاح");
                    return new CustomReponse<CreateTeacherDTO> { StatusCode = 200, Data = TeacherDTO, Message = message };
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CreateTeacherDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var Teacher in e.ConstraintProperties)
                {
                    message.Add($" البريد الالكترونى موجود بالفعل  ");
                }
                return new CustomReponse<CreateTeacherDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CreateTeacherDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<UpdateTeacherDTO> Update([FromForm]UpdateTeacherDTO TeacherDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = iteacher.GetById(TeacherDTO.Id);
                    if(entity is not null)
                    {
                        var data = mapper.Map<Teacher>(TeacherDTO);
                        var imgname = fileUploader.upload("Files", TeacherDTO.Img);
                        data.ImgName = imgname;
                     
                        iteacher.Update(data);
                        var message = new List<string>();
                        message.Add("تم تعديل المعلم بنجاح");
                        return new CustomReponse<UpdateTeacherDTO> { StatusCode = 200, Data = TeacherDTO, Message = message };
                    }
                    else
                    {
                        var NotFoundmessage = new List<string>();
                        NotFoundmessage.Add("المعلم غير موجود");
                        return new CustomReponse<UpdateTeacherDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
                    }
                   
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<UpdateTeacherDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var Teacher in e.ConstraintProperties)
                {
                    message.Add($"  البريد الالكترونى موجود بالفعل  ");
                }
                return new CustomReponse<UpdateTeacherDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<UpdateTeacherDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomReponse<TeacherDTO> Delete(int id)
        {
            var data = iteacher.GetById(id);
            if (data is not null)
            {
                fileUploader.delete(data.ImgName, "Files");
                iteacher.Delete(id);
                var result = mapper.Map<TeacherDTO>(data);
                var message = new List<string>();
                message.Add("تم حذف المعلم بنجاح");
                return new CustomReponse<TeacherDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("المعلم غير موجود");
            return new CustomReponse<TeacherDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
        }


        [HttpGet]
        [Route("Count")]
        public CustomReponse<int> Count()
        {
            var data = iteacher.Count(); ;

            var message = new List<string>();
            message.Add($"  معلمين {data} يوجد  ");
            return new CustomReponse<int> { StatusCode = 200, Data = data, Message = message };

        }
    }
}

using AutoMapper;
using EntityFramework.Exceptions.Common;
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
    public class AcademicYearController : ControllerBase
    {
        private readonly IAcademicYear iacademicYear;
        private readonly IMapper mapper;

        public AcademicYearController(IAcademicYear iacademicYear,IMapper mapper)
        {
            this.iacademicYear = iacademicYear;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<AcademicYearDTO>> GetAll()
        {
            var data = iacademicYear.GetAll();
            var result = mapper.Map<IEnumerable<AcademicYearDTO>>(data);
            var message = new List<string>();
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<AcademicYearDTO>> { StatusCode = 200, Data = result, Message = message };
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomReponse<AcademicYearDTO> GetById(int id)
        {
            var data = iacademicYear.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<AcademicYearDTO>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<AcademicYearDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("الصف الدراسى غير موجود");
            return new CustomReponse<AcademicYearDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<CreateAcademicYearDTO> Create([FromBody]  CreateAcademicYearDTO createAcademicYearDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<AcademicYear>(createAcademicYearDTO);
                    iacademicYear.Add(data);
                    var message = new List<string>();
                    message.Add("تم اضافة الصف الدراسى بنجاح");
                    return new CustomReponse<CreateAcademicYearDTO> { StatusCode = 200, Data = createAcademicYearDTO, Message = message };
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CreateAcademicYearDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var academicYear in e.ConstraintProperties)
                {
                    message.Add($" اسم الصف الدراسى موجود بالفعل  ");
                }
                return new CustomReponse<CreateAcademicYearDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CreateAcademicYearDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<AcademicYearDTO> Update(AcademicYearDTO academicYearDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = iacademicYear.GetById(academicYearDTO.Id);
                    if (entity is not null)
                    {
                        var data = mapper.Map<AcademicYear>(academicYearDTO);
                        iacademicYear.Update(data);
                        var message = new List<string>();
                        message.Add("تم تعديل الصف الدراسي بنجاح ");
                        return new CustomReponse<AcademicYearDTO> { StatusCode = 200, Data = academicYearDTO, Message = message };
                    }
                    else
                    {
                        var NotFoundmessage = new List<string>();
                        NotFoundmessage.Add("الصف الدراسي غير موجودة");
                        return new CustomReponse<AcademicYearDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
                    }

                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<AcademicYearDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var academicyear in e.ConstraintProperties)
                {
                    message.Add($"اسم الصف الدراسى موجود بالفعل  ");
                }
                return new CustomReponse<AcademicYearDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<AcademicYearDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomReponse<AcademicYearDTO> Delete(int id)
        {
            var data = iacademicYear.GetById(id);
            if (data is not null)
            {
                iacademicYear.Delete(id);
                var result = mapper.Map<AcademicYearDTO>(data);
                var message = new List<string>();
                message.Add("تم حذف الصف الدراسى بنجاح");
                return new CustomReponse<AcademicYearDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("الصف الدراسى غير موجود");
            return new CustomReponse<AcademicYearDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
        }


        [HttpGet]
        [Route("Count")]
        public CustomReponse<int> Count()
        {
            var data = iacademicYear.Count(); ;

            var message = new List<string>();
            message.Add($"  صف دراسى {data} يوجد  ");
            return new CustomReponse<int> { StatusCode = 200, Data = data, Message = message };

        }
    }
}

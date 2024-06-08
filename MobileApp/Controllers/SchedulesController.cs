using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileApp.BL.CustomReponse;
using MobileApp.BL.DTO;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.Entities;
using System.Globalization;

namespace MobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly ISchedules schedules;
        private readonly IMapper mapper;

        public SchedulesController(ISchedules schedules, IMapper mapper)
        {
            this.schedules = schedules;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetCourseSchedules/{academicYear}/{Course}/{Teacher}/{Group}")]
        public CustomReponse<IEnumerable<ScheduleDTO>> GetCourseSchedules(int academicYear,int Course,int Teacher,int Group)
        {
            var data = schedules.GetCourseSchedules(academicYear,  Course,  Teacher,Group);
            if (data.Count() != 0)
            {
                var res = mapper.Map<IEnumerable<ScheduleDTO>>(data);
                var Messages = new List<string>();
                Messages.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<IEnumerable<ScheduleDTO>> { StatusCode = 200, Data = res, Message = Messages };
            }
            else
            {
                var res = mapper.Map<IEnumerable<ScheduleDTO>>(data);
                var Messages = new List<string>();
                Messages.Add(" لا يوجد مواعيد");
                return new CustomReponse<IEnumerable<ScheduleDTO>> { StatusCode = 200, Data = res, Message = Messages };
            }
          
        }



        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<ScheduleDTO>> GetAll()
        {
            var data = schedules.GetAll();
            var res = mapper.Map<IEnumerable<ScheduleDTO>>(data);
            var Messages = new List<string>();
            Messages.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<ScheduleDTO>> { StatusCode = 200, Data = res, Message = Messages };
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public CustomReponse<ScheduleDTO> GetById(int id)
        {
            var Messages = new List<string>();
            var data = schedules.GetByID(id);
            if(data is not null)
            {
                var res = mapper.Map<ScheduleDTO>(data);
            
                Messages.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<ScheduleDTO > { StatusCode = 200, Data = res, Message = Messages };
            }
            else
            {
                Messages.Add("ميعاد غير موجود");
                return new CustomReponse<ScheduleDTO> { StatusCode = 404, Data = null, Message = Messages };
            }
           
        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<CreateScheduleDTO> Create(CreateScheduleDTO createSchedule)
        {
            try
            {

            
            var Messages = new List<string>();

            if (ModelState.IsValid)
            {
                  
                    var data = mapper.Map<Schedules>(createSchedule);
                   
                    if (!schedules.IsExist(data))
                    {
                     

                        schedules.Add(data);
                        Messages.Add("تم اضافة الميعاد بنجاح");
                        return new CustomReponse<CreateScheduleDTO> { StatusCode = 200, Data = createSchedule, Message = Messages };
                    }
                    else
                    {
                        Messages.Add("هذا الميعاد مسجل بالفعل");
                        return new CustomReponse<CreateScheduleDTO> { StatusCode = 200, Data = createSchedule, Message = Messages };

                    }

                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();
                return new CustomReponse<CreateScheduleDTO> { StatusCode = 400, Data = null, Message = errors };

            }
            catch(Exception ex)
            {
                var message = new List<string>();
                message.Add(ex.InnerException.Message);
                return new CustomReponse<CreateScheduleDTO> { StatusCode = 400, Data = null, Message = message };
            }
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public CustomReponse<ScheduleDTO> Delete(int id)
        {
            var Messages = new List<string>();
            var data = schedules.GetByID(id);
            if (data is not null)
            {
                var res = mapper.Map<ScheduleDTO>(data);
                schedules.Remove(id);
                Messages.Add("تم حذف البيانات بنجاح");
                return new CustomReponse<ScheduleDTO> { StatusCode = 200, Data = res, Message = Messages };
            }
            else
            {
                Messages.Add("ميعاد غير موجود");
                return new CustomReponse<ScheduleDTO> { StatusCode = 404, Data = null, Message = Messages };
            }

        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<ScheduleDTO> Update(ScheduleDTO ScheduleDTO)
        {
            try
            {


                var Messages = new List<string>();

                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Schedules>(ScheduleDTO);
                    if (!schedules.IsExist(data))
                    {
                        
                       
                        schedules.Update(data);
                        Messages.Add("تم تعديل الميعاد بنجاح");
                        return new CustomReponse<ScheduleDTO> { StatusCode = 200, Data = ScheduleDTO, Message = Messages };
                    }
                    else
                    {
                        Messages.Add("هذا الميعاد مسجل بالفعل");
                        return new CustomReponse<ScheduleDTO> { StatusCode = 200, Data = ScheduleDTO, Message = Messages };

                    }

                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();
                return new CustomReponse<ScheduleDTO> { StatusCode = 400, Data = null, Message = errors };

            }
            catch (Exception ex)
            {
                var message = new List<string>();
                message.Add(ex.InnerException.Message);
                return new CustomReponse<ScheduleDTO> { StatusCode = 400, Data = null, Message = message };
            }
        }

    }
}

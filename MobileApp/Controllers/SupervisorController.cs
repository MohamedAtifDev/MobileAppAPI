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
    public class SupervisorController : ControllerBase
    {
        private readonly ISupervisor isupervisor;
        private readonly IMapper mapper;
        //private readonly DataContext db;

        public SupervisorController(ISupervisor isupervisor, IMapper mapper)//, DataContext db)
        {
            this.isupervisor = isupervisor;
            this.mapper = mapper;
            //this.db = db;
        }

        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<SupervisorDTO>> GetAll()
        {
            var data = isupervisor.GetAll();
            var result = mapper.Map<IEnumerable<SupervisorDTO>>(data);
            var message = new List<string>();
            message.Add("Data Retrieved Successfully");
            return new CustomReponse<IEnumerable<SupervisorDTO>> { StatusCode = 200, Data = result, Message = message };


        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomReponse<SupervisorDTO> GetById(int id)
        {
            var data = isupervisor.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<SupervisorDTO>(data);
                var message = new List<string>();
                message.Add("Data Retrieved Successfully");
                return new CustomReponse<SupervisorDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("Supervisor Not Found");
            return new CustomReponse<SupervisorDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<CreateSupervisorDTO> Create([FromBody] CreateSupervisorDTO CreateSupervisorDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Supervisor>(CreateSupervisorDTO);
                    isupervisor.Add(data);
                    var message = new List<string>();
                    message.Add("Supervisor Added Successfully");
                    return new CustomReponse<CreateSupervisorDTO> { StatusCode = 200, Data = CreateSupervisorDTO, Message = message };
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CreateSupervisorDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var Teacher in e.ConstraintProperties)
                {
                    message.Add(Teacher + " is Already Used");
                }
                return new CustomReponse<CreateSupervisorDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CreateSupervisorDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<SupervisorDTO> Update(SupervisorDTO SupervisorDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = isupervisor.GetById(SupervisorDTO.Id);
                    if(entity is not null)
                    {
                        var data = mapper.Map<Supervisor>(SupervisorDTO);
                        isupervisor.Update(data);
                        var message = new List<string>();
                        message.Add("Supervisor Updated Successfully");
                        return new CustomReponse<SupervisorDTO> { StatusCode = 200, Data = SupervisorDTO, Message = message };
                    }
                    else
                    {
                        var NotFoundmessage = new List<string>();
                        NotFoundmessage.Add("Supervisor Not Found");
                        return new CustomReponse<SupervisorDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
                    }
                 
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<SupervisorDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var teacher in e.ConstraintProperties)
                {
                    message.Add(teacher + " is Already Used");
                }
                return new CustomReponse<SupervisorDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<SupervisorDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomReponse<SupervisorDTO> Delete(int id)
        {
            var data = isupervisor.GetById(id);
            if (data is not null)
            {
                isupervisor.Delete(id);
                var result = mapper.Map<SupervisorDTO>(data);
                var message = new List<string>();
                message.Add("Data Deleted Successfully");
                return new CustomReponse<SupervisorDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("Supervisor Not Found");
            return new CustomReponse<SupervisorDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
        }


        [HttpGet]
        [Route("Count")]
        public CustomReponse<int> Count()
        {
            var data = isupervisor.Count(); ;

            var message = new List<string>();
            message.Add($"there is {data} record in table Supervisor");
            return new CustomReponse<int> { StatusCode = 200, Data = data, Message = message };

        }
    }
}

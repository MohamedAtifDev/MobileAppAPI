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
    public class GroupController : ControllerBase
    {
        private readonly IGroup grp;
        private readonly IMapper mapper;

        public GroupController(IGroup grp,IMapper mapper)
        {
            this.grp = grp;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<GroupDTO>> GetAll()
        {
            var data = grp.GetAll();
            var result = mapper.Map<IEnumerable<GroupDTO>>(data);
            var message = new List<string>();
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<GroupDTO>> { StatusCode = 200, Data = result, Message = message };
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomReponse<GroupDTO> GetById(int id)
        {
            var data = grp.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<GroupDTO>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<GroupDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("المجموعة غير موجود");
            return new CustomReponse<GroupDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<CreateGroupDTO> Create([FromBody] CreateGroupDTO CreateGroupDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Group>(CreateGroupDTO);
                    grp.Add(data);
                    var message = new List<string>();
                    message.Add("تم اضافة المجموعة بنجاح");
                    return new CustomReponse<CreateGroupDTO> { StatusCode = 200, Data = CreateGroupDTO, Message = message };
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CreateGroupDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var academicYear in e.ConstraintProperties)
                {
                    message.Add($"  موجودة بالفعل {academicYear} المجموعة ");
                }
                return new CustomReponse<CreateGroupDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CreateGroupDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<GroupDTO> Update(GroupDTO group)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = grp.GetById(group.Id);
                    if (entity is not null)
                    {
                        var data = mapper.Map<Group>(group);
                        grp.Update(data);
                        var message = new List<string>();
                        message.Add("تم تعديل المجموعة بنجاح ");
                        return new CustomReponse<GroupDTO> { StatusCode = 200, Data = group, Message = message };
                    }
                    else
                    {
                        var NotFoundmessage = new List<string>();
                        NotFoundmessage.Add("المجموعة غير موجودة");
                        return new CustomReponse<GroupDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
                    }

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                 .Select(e => e.ErrorMessage)
                                                 .ToList();
                    return new CustomReponse<GroupDTO> { StatusCode = 400, Data = null, Message = errors };
                }
     


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var academicyear in e.ConstraintProperties)
                {
                    message.Add($"  موجودة بالفعل {academicyear} االمجموعة ");
                }
                return new CustomReponse<GroupDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<GroupDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomReponse<GroupDTO> Delete(int id)
        {
            var data = grp.GetById(id);
            if (data is not null)
            {
                grp.Delete(id);
                var result = mapper.Map<GroupDTO>(data);
                var message = new List<string>();
                message.Add("تم حذف المجموعة بنجاح");
                return new CustomReponse<GroupDTO> { StatusCode = 200, Data = result, Message = message };
            }
            else
            {
                var NotFoundmessage = new List<string>();
                NotFoundmessage.Add("المجموعة غير موجود");
                return new CustomReponse<GroupDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
            }
           
        }


        [HttpGet]
        [Route("Count")]
        public CustomReponse<int> Count()
        {
            var data = grp.Count(); ;

            var message = new List<string>();
            message.Add($"  مجموعات {data} يوجد  ");
            return new CustomReponse<int> { StatusCode = 200, Data = data, Message = message };

        }
    }
}

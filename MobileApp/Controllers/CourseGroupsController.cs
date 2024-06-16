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
    public class CourseGroupsController : ControllerBase
    {
        private readonly ICourseGroup Crs_Grp;
        private readonly IMapper mapper;

        public CourseGroupsController(ICourseGroup Crs_grp ,IMapper mapper)
        {
            Crs_Grp = Crs_grp;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<CourseGroupsDTO>> GetAll()
        { var message = new List<string>();
            var data = Crs_Grp.GetAll();
            var res = mapper.Map<IEnumerable<CourseGroupsDTO>>(data);
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<CourseGroupsDTO>> { Data = res ,StatusCode=200,Message=message};

        }

        [HttpGet]
        [Route("GetGroups/{AcademicYear}/{Course}/{Teacher}")]

        public CustomReponse<IEnumerable<GetGroupsDTO>> GetGroups(int AcademicYear, int Course, int Teacher)
        {
            var message = new List<string>();
            var data = Crs_Grp.GetGroups(AcademicYear,Course,Teacher);
          
            message.Add("تم استرجاع البيانات بنجاح");
            return new CustomReponse<IEnumerable<GetGroupsDTO>> { Data = data, StatusCode = 200, Message = message };

        }

        [HttpPost]
        [Route("Create")]

        public CustomReponse<CourseGroupsDTO> Create(CourseGroupsDTO courseGroupDTO)
        {
            var message = new List<string>();
            if (ModelState.IsValid)

            {
              
                var data=mapper.Map<CourseGroups>(courseGroupDTO);
                if (Crs_Grp.isExist(data.AcademicYearId,data.CourseId,data.TeacherId,(int)data.GroupId))
                {
                    message.Add("البيانات موجودة بالفعل");
                    return new CustomReponse<CourseGroupsDTO> { Data = courseGroupDTO, StatusCode = 400, Message = message };
                }
                else
                {
                    Crs_Grp.Create(data);
                    message.Add("تم اضافة البيانات بنجاح");
                    return new CustomReponse<CourseGroupsDTO> { Data = courseGroupDTO, StatusCode = 200, Message = message };
                }
              

            }
            else
            {
                var errors=ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage).ToList();
                return new CustomReponse<CourseGroupsDTO> { Data = null, StatusCode = 400, Message =errors};


            }
        }







        [HttpDelete]
        [Route("Delete")]
        public CustomReponse<CourseGroupsDTO> Delete(CourseGroupsDTO courseGroupsDTO) {
            var message = new List<string>();
            try
            {
              

                if (Crs_Grp.isExist(courseGroupsDTO.AcademicYearId, courseGroupsDTO.CourseId, courseGroupsDTO.TeacherId, courseGroupsDTO.GroupId))
                {
                    var data = mapper.Map<CourseGroups>(courseGroupsDTO);
                    Crs_Grp.Delete(data);
                    message.Add("تم حذف البيانات بنجاح");
                    return new CustomReponse<CourseGroupsDTO>
                    {
                        Data = null,
                        StatusCode = 200,
                        Message = message
                    };
                }
                else
                {
                    message.Add("البيانات غير موجودة");
                    return new CustomReponse<CourseGroupsDTO>
                    {
                        Data = null,
                        StatusCode =404,
                        Message = message
                    };
                }
            }catch (Exception ex)
            {
                message.Add(ex.Message);
                return new CustomReponse<CourseGroupsDTO>
                {
                    Data = null,
                    StatusCode = 404,
                    Message = message
                };
            }
        
        }




        [HttpPut]
        [Route("Update")]

        public CustomReponse<UpdateCourseGroupDTO> Update(UpdateCourseGroupDTO courseGroupDTO)
        {
            var message = new List<string>();
            if (ModelState.IsValid)
            { 
                if(courseGroupDTO.NewGroupId == null) { 
                courseGroupDTO.NewGroupId=courseGroupDTO.GroupId;
                }

            

                var data = mapper.Map<CourseGroups>(courseGroupDTO);
                if (Crs_Grp.isExist(data.AcademicYearId, data.CourseId, data.TeacherId,(int)courseGroupDTO.NewGroupId))
                {
                    message.Add("البيانات موجودة بالفعل");
                    return new CustomReponse<UpdateCourseGroupDTO> { Data = courseGroupDTO, StatusCode = 400, Message = message };
                }
                else
                {
                    Crs_Grp.update(courseGroupDTO);
                    message.Add("تم تعديل البيانات بنجاح");
                    return new CustomReponse<UpdateCourseGroupDTO> { Data = courseGroupDTO, StatusCode = 200, Message = message };
                }
          

            }
            else
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return new CustomReponse<UpdateCourseGroupDTO> { Data = null, StatusCode = 400, Message = errors };


            }
        }
    }
}

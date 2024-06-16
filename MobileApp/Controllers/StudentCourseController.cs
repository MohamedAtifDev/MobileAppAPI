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
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCourse std_Crs;
        private readonly IMapper mapper;
        private readonly IAcademicYeatCoursesTeachers academicYeatCoursesTeachers;

        public StudentCourseController(IStudentCourse std_crs,IMapper mapper,IAcademicYeatCoursesTeachers academicYeatCoursesTeachers)
        {
            std_Crs = std_crs;
            this.mapper = mapper;
            this.academicYeatCoursesTeachers = academicYeatCoursesTeachers;
        }
        [HttpGet]
            [Route("GetAll")]
            public CustomReponse<IEnumerable<StudentCourseDTO>> GetAll()
            {
                var data = std_Crs.GetAll();
               var result = mapper.Map<IEnumerable<StudentCourseDTO>>(data);
                var message = new List<string>();
                message.Add("تم استرجاع البيانات بنجاح");
                return new CustomReponse<IEnumerable<StudentCourseDTO>> { StatusCode = 200, Data = result, Message = message };
            }

            //[HttpGet]
            //[Route("GetById")]
            //public CustomReponse<StudentCourseDTO> GetById(StudentCourseDTO studentCourse)
            //{
            //var res=mapper.Map<StudentCourse>(studentCourse);    
            //    var data = std_Crs.GetById(res);
            //    if (data is not null)
            //    {
            //        var result = mapper.Map<StudentCourseDTO>(data);
            //        var message = new List<string>();
            //        message.Add("تم استرجاع البيانات بنجاح");
            //        return new CustomReponse<StudentCourseDTO> { StatusCode = 200, Data = result, Message = message };
            //    }
            //    var NotFoundmessage = new List<string>();
            //    NotFoundmessage.Add("الطالب غير مسجل في المادة");
            //    return new CustomReponse<StudentCourseDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

            //}

            [HttpPost]
            [Route("Create")]
            public CustomReponse<StudentCourseDTO> Create([FromBody] StudentCourseDTO StudentCourseDTO)
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                   
                    var res = mapper.Map<StudentCourse>(StudentCourseDTO);
                    var record=std_Crs.IsExsits(res);
                    if(!record)
                    {
                        var data = mapper.Map<StudentCourse>(StudentCourseDTO);
                        std_Crs.Add(data);
                        var message = new List<string>();
                        message.Add("تم اضافة البيانات بنجاح");
                        return new CustomReponse<StudentCourseDTO> { StatusCode = 200, Data = StudentCourseDTO, Message = message };

                    }
                    else
                    {
                        var message = new List<string>();
                        message.Add("الطالب مسجل في المادة بالفعل");
                        return new CustomReponse<StudentCourseDTO> { StatusCode = 400, Data = StudentCourseDTO, Message = message };

                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                             .Select(e => e.ErrorMessage)
                                                             .ToList();
                    return new CustomReponse<StudentCourseDTO> { StatusCode = 400, Data = null, Message = errors };
                }
                 


                }
                //catch (UniqueConstraintException e)
                //{
                //    var message = new List<string>();
                //    foreach (var academicYear in e.ConstraintProperties)
                //    {
                //        message.Add($"  موجودة بالفعل {academicYear} المجموعة ");
                //    }
                //    return new CustomReponse<StudentCourseDTO> { StatusCode = 400, Data = null, Message = message };

                //}
                catch (Exception e)
                {
                    var message = new List<string>();
                    message.Add(e.InnerException.Message);
                    return new CustomReponse<StudentCourseDTO> { StatusCode = 400, Data = null, Message = message };

                }
            }
            [HttpPut]
            [Route("Update")]
            public CustomReponse<StudentCourseDTO> Update(UpdateStudentCourse updateStudentCourse)
            {
                try
                {
                    if (ModelState.IsValid)
                    {

                    //var res = mapper.Map<StudentCourse>(updateStudentCourse);

                    var ToDelete = new StudentCourse
                    {
                        AcademicYearId = updateStudentCourse.Old_AcademicYear,
                        CourseId = updateStudentCourse.old_CourseId,
                        StudentId = updateStudentCourse.StudentId,
                        TeacherId = updateStudentCourse.old_TeacherID,
                        GroupID=updateStudentCourse.Old_GroupId
                    };
                    var toAdd = new StudentCourse
                    {
                        AcademicYearId = updateStudentCourse.New_AcademicYear==null ?updateStudentCourse.Old_AcademicYear : (int)updateStudentCourse.New_AcademicYear,
                        CourseId = updateStudentCourse.New_CourseId == null ? updateStudentCourse.old_CourseId :(int) updateStudentCourse.New_CourseId,
                        TeacherId = updateStudentCourse.New_TeacherID == null ? updateStudentCourse.old_TeacherID : (int)updateStudentCourse.New_TeacherID,
                        StudentId = updateStudentCourse.StudentId,
                        GroupID= updateStudentCourse.New_GroupId == null ? updateStudentCourse.Old_GroupId : (int)updateStudentCourse.New_GroupId

                    };
                 
                    var isexist = std_Crs.IsExsits(toAdd);
                        if ((isexist && updateStudentCourse.Old_GroupId!=updateStudentCourse.New_GroupId)||!isexist)
                        {
                            
                            std_Crs.Delete(ToDelete);
                     
                             var final = mapper.Map<StudentCourse>(toAdd);
                            std_Crs.Add(final);
                            var message = new List<string>();
                            message.Add("تم تعديل البيانات بنجاح ");
                            return new CustomReponse<StudentCourseDTO> { StatusCode = 200, Data = null, Message = message };
                        }
                 
                        else
                        {
                            var AlreadyExist = new List<string>();
                        AlreadyExist.Add("الطالب مسجل بالفعل في المادة");
                            return new CustomReponse<StudentCourseDTO> { StatusCode = 404, Data = null, Message = AlreadyExist };
                        }

                    }
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();
                    return new CustomReponse<StudentCourseDTO> { StatusCode = 400, Data = null, Message = errors };


                }
             
                catch (Exception e)
                {
                    var message = new List<string>();
                    message.Add(e.InnerException.Message);
                    return new CustomReponse<StudentCourseDTO> { StatusCode = 400, Data = null, Message = message };

                }
            }

            [HttpDelete]
            [Route("Delete")]
            public CustomReponse<StudentCourseDTO> Delete(StudentCourseDTO studentCourse)
            {
            var res = mapper.Map<StudentCourse>(studentCourse);
            var data = std_Crs.IsExsits(res);
                if (data )
                {
                    std_Crs.Delete(res);
                    var result = mapper.Map<StudentCourseDTO>(res);
                    var message = new List<string>();
                    message.Add("تم حذف السجل بنجاح");
                    return new CustomReponse<StudentCourseDTO> { StatusCode = 200, Data = result, Message = message };
                }
                var NotFoundmessage = new List<string>();
                NotFoundmessage.Add("الطالب غير مسجل في المادة");
                return new CustomReponse<StudentCourseDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
            }


            [HttpGet]
            [Route("Count")]
            public CustomReponse<int> Count()
            {
                var data = std_Crs.Count(); ;

                var message = new List<string>();
                message.Add($"  سجلات {data} يوجد  ");
                return new CustomReponse<int> { StatusCode = 200, Data = data, Message = message };

            }


        [HttpGet]
        [Route("GetStudentCourses/{studenid}")]
        public CustomReponse<IEnumerable<MyCoursesDTO>> GetStudentCourses(string studenid)
        {
            var message = new List<string>();
            var data = std_Crs.GetStudentCourses(studenid);
            if (data.Count()==0)
            {
                message.Add("لا يوجد مواد حتي الان");

                return new CustomReponse<IEnumerable<MyCoursesDTO>> { StatusCode = 404, Data = null, Message = message };
            }
            else
            {
                message.Add("تم استرجاع المواد بنجاح");
                return new CustomReponse<IEnumerable<MyCoursesDTO>> { StatusCode = 200, Data = data, Message = message };

            }

        }

        [HttpGet]
        [Route("GetStudentCoursesSchedules/{studenid}")]
        public CustomReponse<IEnumerable<MyCoursesDTO>> GetStudentCoursesSchedules(string studenid)
        {
            var message = new List<string>();
            var data = std_Crs.GetStudentCoursesSchedules(studenid);
            if (data.Count()==0)
            {
                message.Add("لا يوجد مواد حتي الان");

                return new CustomReponse<IEnumerable<MyCoursesDTO>> { StatusCode = 404, Data = null, Message = message };
            }
            else
            {
                message.Add("تم استرجاع المواد بنجاح");
                return new CustomReponse<IEnumerable<MyCoursesDTO>> { StatusCode = 200, Data = data, Message = message };

            }

        }

    }
}

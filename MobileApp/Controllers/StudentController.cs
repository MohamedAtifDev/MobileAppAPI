using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileApp.BL.CustomReponse;
using MobileApp.BL.DTO;
using MobileApp.BL.Interfaces;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

namespace MobileApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent istudent;
        private readonly IMapper mapper;
        private readonly DataContext db;

        public StudentController(IStudent istudent, IMapper mapper,DataContext db)
        {
            this.istudent = istudent;
            this.mapper = mapper;
            this.db = db;
        }

        [HttpGet]
        [Route("GetAll")]
        public CustomReponse<IEnumerable<StudentDTO>> GetAll()
        {
            var data = istudent.GetAll();
            var result = mapper.Map<IEnumerable<StudentDTO>>(data);
            var message = new List<string>();
            message.Add("Data Retrieved Successfully");
            return new CustomReponse<IEnumerable<StudentDTO>> { StatusCode = 200, Data = result, Message = message };


        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomReponse<StudentDTO> GetById(int id)
        {
            var data = istudent.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<StudentDTO>(data);
                var message = new List<string>();
                message.Add("Data Retrieved Successfully");
                return new CustomReponse<StudentDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("Student Not Found");
            return new CustomReponse<StudentDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };

        }

        [HttpPost]
        [Route("Create")]
        public CustomReponse<CreateStudentDTO> Create([FromBody] CreateStudentDTO CreateStudentDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Student>(CreateStudentDTO);
                    istudent.Add(data);
                    var message = new List<string>();
                    message.Add("Student Added Successfully");
                    return new CustomReponse<CreateStudentDTO> { StatusCode = 200, Data = CreateStudentDTO, Message = message };
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<CreateStudentDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var student in e.ConstraintProperties)
                {
                    message.Add(student + " is Already Used");
                }
                return new CustomReponse<CreateStudentDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<CreateStudentDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }
        [HttpPut]
        [Route("Update")]
        public CustomReponse<StudentDTO> Update(StudentDTO StudentDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = istudent.GetById(StudentDTO.Id);
                    if(entity is not null)
                    {
                        var data = mapper.Map<Student>(StudentDTO);
                        istudent.Update(data);
                        var message = new List<string>();
                        message.Add("Student Updated Successfully");
                        return new CustomReponse<StudentDTO> { StatusCode = 200, Data = StudentDTO, Message = message };
                    }
                    else
                    {
                        var NotFoundmessage = new List<string>();
                        NotFoundmessage.Add("Student Not Found");
                        return new CustomReponse<StudentDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
                    }
                    
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                return new CustomReponse<StudentDTO> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (UniqueConstraintException e)
            {
                var message = new List<string>();
                foreach (var student in e.ConstraintProperties)
                {
                    message.Add(student +" is Already Used");
                }
                return new CustomReponse<StudentDTO> { StatusCode = 400, Data = null, Message = message };

            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<StudentDTO> { StatusCode = 400, Data = null, Message = message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomReponse<StudentDTO> Delete(int id)
        {
            var data = istudent.GetById(id);
            if (data is not null)
            {
                istudent.Delete(id);
                var result = mapper.Map<StudentDTO>(data);
                var message = new List<string>();
                message.Add("Data Deleted Successfully");
                return new CustomReponse<StudentDTO> { StatusCode = 200, Data = result, Message = message };
            }
            var NotFoundmessage = new List<string>();
            NotFoundmessage.Add("Student Not Found");
            return new CustomReponse<StudentDTO> { StatusCode = 404, Data = null, Message = NotFoundmessage };
        }


        [HttpGet]
        [Route("Count")]
        public CustomReponse<int> Count()
        {
            var data = istudent.Count(); ;
           
            var message = new List<string>();
            message.Add($"there is {data} record in table Student");
            return new CustomReponse<int> { StatusCode = 200, Data = data, Message = message };
         
        }
        //[HttpGet]
        //[Route("Getdata")]
        //public CustomReponse<object> getdata()
        //{
        //    var data = db.AcademicYears.Include(a=>a.AcademicYearCourses).ThenInclude(a=>a.Teacher).Include(a => a.AcademicYearCourses).ThenInclude(a => a.Course);
        //    var res = data.Select(a => new trying
        //    {
        //        id = a.Id,
        //        name = a.Name,
        //        courseDTos = a.AcademicYearCourses.Select(Crs => Crs.Course).Select(crsDTO => new CourseDTo
        //        {
        //            id = crsDTO.Id,
        //            name = crsDTO.Name,
        //            teacher = new TeacherDTO
        //            {
        //                id = crsDTO.academicYearCourses.Where(vcxz => a.Id == vcxz.AcademicYearId && vcxz.CourseId == crsDTO.Id).Select(a => a.Teacher.Id).ToArray()[0],                   //teacherid = a.academicYearCourses.Where(b => b.Course.Id == a.Id).Select(a => a.TeacherId).FirstOrDefault(),
        //                name = crsDTO.academicYearCourses.Where(vcxz => a.Id == vcxz.AcademicYearId && vcxz.CourseId == crsDTO.Id).Select(a => a.Teacher.Name).ToArray()[0]
        //            }


        //            //teacherName= a.academicYearCourses.Where(b => b.Course.Id == a.Id).Select(a => a.Teacher.Name).FirstOrDefault()

        //        })
        //        //List<object> bb = new List<object>();
        //        //foreach (var item in data)
        //        //{ var bbc = item.Id;

        //        //    var nn = new trying
        //        //    {
        //        //        id = item.Id,
        //        //        name = item.Name,
        //        //        courseDTos = item.AcademicYearCourses.Select(a => a.Course).Select(crs => new CourseDTo
        //        //        {
        //        //            id = crs.Id,
        //        //            name = crs.Name,
        //        //            teacher = new TeacherDTO
        //        //            {
        //        //                id = crs.academicYearCourses.Where(a => a.AcademicYearId == bbc && crs.Id == a.CourseId).Select(a => a.Teacher.Id).ToList()[0],
        //        //                name = crs.academicYearCourses.Where(a => a.AcademicYearId == bbc && crs.Id == a.CourseId).Select(a => a.Teacher.Name).ToList()[0]
        //        //            }
        //        //        })
        //        //    };
        //        //    bb.Add(nn);
        //    });


           
        //    return new CustomReponse<object> { StatusCode = 200, Data =  res};

        //}
    }
}

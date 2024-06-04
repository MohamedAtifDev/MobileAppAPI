using AutoMapper;
using GraduationProjectAPI.BL.Services;
using GraduationProjectAPI.BL.VM;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using MobileApp.BL.CustomReponse;

using MobileApp.BL.DTO;

using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;

using Microsoft.EntityFrameworkCore;
using MobileApp;
using MobileApp.BL.Interfaces;



namespace OnlineExamAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [EnableCors]
    public class AccountController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly UserManager<AppUser> usermanager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<AppUser> hasher;


        public AccountController(DataContext dataContext, UserManager<AppUser> usermanager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, IMapper mapper, IPasswordHasher<AppUser> hasher)
        {
            this.dataContext = dataContext;
            this.usermanager = usermanager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.hasher = hasher;
            this.roleManager = roleManager;
        }

        [HttpPost]
        [Route("AdminSignUp")]
        public async Task<CustomReponse<UserDTO>> AdminSignUp([FromBody] AdminSignUpDTO sign)
        {
            try
            {
                var message = new List<string>();
                if (ModelState.IsValid)
                {
                    var user = new AppUser
                    {
                        Email = sign.Email,
                        DisplayName = sign.UserName,
                        PhoneNumber = sign.Phone,

                        UserName = sign.UserName + GenereateToken().ToString()


                    };


                    var data = await usermanager.FindByEmailAsync(sign.Email);
                    if (data != null)
                    {
                        message.Add("البريد الالكترونى مسجل بالفعل");
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = message };
                    }
                    else
                    {
                        var result = await usermanager.CreateAsync(user, sign.Password);
                        if (result.Succeeded)
                        {
                            var role = await roleManager.FindByIdAsync("2");
                            var AddToRole = await usermanager.AddToRoleAsync(user, role.Name);

                            if (AddToRole.Succeeded)
                            {
                                message.Add("تم الاشتراك بنجاح");


                                var userdata = new UserDTO
                                {
                                    Email = user.Email,
                                    UserName = user.DisplayName,
                                    RoleName = "Admin"
                                };
                                return new CustomReponse<UserDTO> { StatusCode = 200, Message = message, Data = userdata };
                            }

                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                message.Add(error.Description);
                            }

                            return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = null };

                        }
                    }



                }


                foreach (var item in ModelState.Values)
                {
                    foreach (var item2 in item.Errors)
                    {
                        message.Add(item2.ErrorMessage);
                    }
                }
                return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = null };



            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<UserDTO> { StatusCode = 500, Message = message, Data = null };
            }


        }

        [HttpGet]
        [Route("GetMyAccount")]
        public async Task<IActionResult> GetMyAccount(string Email)
        {
            var data =await usermanager.FindByEmailAsync(Email);
            if(data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
           

        }


        [HttpPost]
        [Route("UserSignUp")]
        public async Task<CustomReponse<UserDTO>> UserSignUp([FromBody] UserSignUpDTO sign)
        {
            try
            {
                var message = new List<string>();
                if (ModelState.IsValid)
                {
                    var data = await usermanager.FindByEmailAsync(sign.Email);
                    if (data != null)
                    {
                        message.Add("تم تسجيل الدخول بنجاح");
                        return new CustomReponse<UserDTO> { StatusCode = 200, Message = message, Data = null };

                    }
                    else
                    {
                        var user = new AppUser
                        {
                            Email = sign.Email,

                            DisplayName = sign.UserName,

                            PhoneNumber = sign.Phone,
                            UserName = sign.UserName + GenereateToken().ToString()



                        };
                        var Loginresult = await usermanager.CreateAsync(user, "12345Mm@");
                        if (Loginresult.Succeeded)
                        {
                            var role = await roleManager.FindByIdAsync("1");
                            var AddToRole = await usermanager.AddToRoleAsync(user, role.Name);

                            if (AddToRole.Succeeded)
                            {
                                message.Add("تم الاشتراك بنجاح");


                                var userdata = new UserDTO
                                {
                                    Email = user.Email,
                                    UserName = user.DisplayName,
                                    RoleName = "User"
                                };
                                return new CustomReponse<UserDTO> { StatusCode = 200, Message = message, Data = userdata };
                            }

                        }
                        else
                        {
                            foreach (var error in Loginresult.Errors)
                            {
                                message.Add(error.Description);
                            }
                            return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = null };

                        }

                    }



                }


                foreach (var item in ModelState.Values)
                {
                    foreach (var item2 in item.Errors)
                    {
                        message.Add(item2.ErrorMessage);
                    }
                }
                return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = null };



            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<UserDTO> { StatusCode = 500, Message = message, Data = null };
            }


        }



        //[HttpPost]
        //[Route("UserSignIn")]
        //public async Task<CustomReponse<UserDTO>> UserSignIn([FromBody] SignInDTO sign)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var user = await usermanager.FindByEmailAsync(sign.Email);
        //           // var result = await signInManager.PasswordSignInAsync(user.UserName, sign.Password, sign.remember == null ? false :true, false);




        //            if (user!=null)
        //            {
        //                var IsInRole = await usermanager.IsInRoleAsync(user, "User");

        //                if (IsInRole)
        //                {
        //                    var messages = new string[]
        //              {
        //                       "تم تسجيل الدخول بنجاح"
        //              }.ToList();

        //                    var userdata = new UserDTO
        //                    {
        //                        Email = user.Email,
        //                        RoleName = "User"
        //                    };
        //                    return new CustomReponse<UserDTO> { StatusCode = 200, Message = messages, Data = userdata };
        //                }
        //                else
        //                {
        //                    var invalidmessages = new string[]
        //                                       {
        //                       "كلمه المرور او البريد الالكترونى غير صحيح"
        //                                       }.ToList();
        //                    return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data = null};
        //                }

        //            }
        //            else
        //            {
        //                var invalidmessages = new string[]
        //               {
        //                            "كلمه المرور او البريد الالكترونى غير صحيح"
        //               }.ToList();
        //                return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data =null };

        //            }

        //        }

        //        var message = new List<string>(); ;
        //        foreach (var item in ModelState.Values)
        //        {
        //            foreach (var item2 in item.Errors)
        //            {
        //                message.Add(item2.ErrorMessage);
        //            }
        //        }
        //        return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = null };



        //    }
        //    catch (Exception e)
        //    {
        //        var message = new List<string>();
        //        message.Add(e.InnerException.Message);
        //        return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = null };
        //    }


        //}

        [HttpPost]
        [Route("AdminSignIn")]

        public async Task<CustomReponse<UserDTO>> AdminSignIn([FromBody] SignInDTO sign)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await usermanager.FindByEmailAsync(sign.Email);
                    var result = await signInManager.PasswordSignInAsync(user.UserName, sign.Password, sign.remember == null ? false : true, false);




                    if (result.Succeeded)
                    {
                        var IsInRole = await usermanager.IsInRoleAsync(user, "Admin") || await usermanager.IsInRoleAsync(user, "SuperAdmin");

                        if (IsInRole)
                        {
                            var messages = new string[]
                      {
                                   "تم تسجيل الدخول بنجاح"
                      }.ToList();

                            var userdata = new UserDTO
                            {
                                Email = user.Email,
                                RoleName = (await usermanager.GetRolesAsync(user)).ElementAt(0)
                            };
                            return new CustomReponse<UserDTO> { StatusCode = 200, Message = messages, Data = userdata };
                        }
                        else
                        {

                            var invalidmessages = new string[]
                                               {
                                    "كلمه المرور او البريد الالكترونى غير صحيح"
                                               }.ToList();
                            return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data = null };
                        }

                    }
                    else
                    {

                        var invalidmessages = new string[]
                       {
                                    "كلمه المرور او البريد الالكترونى غير صحيح"
                       }.ToList();
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data = null };

                    }

                }

                var message = new List<string>(); ;
                foreach (var item in ModelState.Values)
                {
                    message.Add(item.Errors.ToString());
                }
                return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = null };



            }
            catch (Exception e)
            {
                var message = new List<string>();
                message.Add(e.InnerException.Message);
                return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = null };
            }


        }



        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<CustomReponse<string>> ForgetPassword(ForgetPasswordDTO fg)
        {
            if (ModelState.IsValid)
            {
                var user = await usermanager.FindByEmailAsync(fg.Email);

                if (user is not null)
                {

                    var token = GenereateToken();
                    user.Token = token;
                    await usermanager.UpdateAsync(user);
                    var email = $"  {token} رمز التحقق الخاص بك هو ";

                    var state = MailSender.sendmail(fg.Email, email);
                    var message = new List<string>();
                    if (state.Result)
                    {

                        message.Add("تم ارسال رمز التحقق بنجاح");
                        return new CustomReponse<string> { StatusCode = 200, Message = message, Data = token.ToString() };
                    }
                    else
                    {
                        message.Add("خطا في النظام");
                        return new CustomReponse<string> { StatusCode = 500, Message = message, Data = null };
                    }


                }
                else
                {

                    var message = new List<string>();
                    message.Add("المستخدم غير موجود");
                    return new CustomReponse<string> { StatusCode = 404, Message = message, Data = null };
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();

                return new CustomReponse<string> { StatusCode = 400, Data = null, Message = errors };

            }
        }
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<CustomReponse<string>> ResetPassword(ResetPasswordDTO resetPassword)
        {
            var message = new List<string>();
            if (ModelState.IsValid)
            {
                var user = await usermanager.FindByEmailAsync(resetPassword.Email);

                if (user != null)
                {
                    if (user.Token == int.Parse(resetPassword.token))
                    {

                        String hashedNewPassword = usermanager.PasswordHasher.HashPassword(user, resetPassword.password);
                        user.PasswordHash = hashedNewPassword;
                        var updateResult = await usermanager.UpdateAsync(user);

                        if (updateResult.Succeeded)
                        {
                            message.Add("تم اعادة تعيين كلمة المرور بنجاح");
                            return new CustomReponse<string> { StatusCode = 200, Message = message, Data = null };
                        }
                        else
                        {
                            message.Add("خطا في النظام");
                            return new CustomReponse<string> { StatusCode = 500, Message = message, Data = null };
                        }
                    }
                    else
                    {
                        message.Add("رمز تحقق غير صحيح");
                        return new CustomReponse<string> { StatusCode = 500, Message = message, Data = null };
                    }
                }
                else
                {

                    message.Add("مستخدم غير موجود");
                    return new CustomReponse<string> { StatusCode = 404, Message = message, Data = null };


                }



            }


            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();

                return new CustomReponse<string> { StatusCode = 400, Data = null, Message = errors };
            }
        }








        [HttpPut]
        [Route("UpdateAdmin")]

        public async Task<CustomReponse<AppUser>> UpdateAdmin([FromBody] UpdateAdminDTO App)
        {
            var message = new List<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    var userdata = dataContext.Users.Where(a => a.Id == App.Id).AsNoTracking().FirstOrDefault();
                    var user = dataContext.Users.Where(a => a.Email == App.Email).AsNoTracking().FirstOrDefault();
                    var user2 = dataContext.Users.Where(a => a.DisplayName == App.UserName).AsNoTracking().FirstOrDefault();
                    if (userdata is not null)
                    {
                        if (user is not null)
                        {
                            if (user.Id != App.Id)
                            {
                                message.Add("البريد الالكترونى موجود بالفعل");
                                return new CustomReponse<AppUser> { StatusCode = 400, Data = user, Message = message };
                            }

                            else
                            {
                                var checker = new CustomPasswordValidator<AppUser>();
                                var result = await checker.ValidateAsync(usermanager, userdata, App.Password);
                                if (!result.Succeeded)
                                {
                                    foreach (var item in result.Errors)
                                    {
                                        message.Add(item.Description);
                                    }
                                    return new CustomReponse<AppUser> { StatusCode = 400, Data = user, Message = message };

                                }
                                else
                                {

                                    userdata.Email = App.Email;
                                    userdata.PhoneNumber = App.Phone;
                                    userdata.DisplayName = App.UserName;
                                    userdata.UserName = App.UserName + GenereateToken().ToString();
                                    userdata.PasswordHash = hasher.HashPassword(userdata, App.ConfirmPassword);

                                    dataContext.ChangeTracker.Clear();
                                    dataContext.Users.Update(userdata);
                                    dataContext.SaveChanges();

                                    message.Add("تم تعديل المشرف بنجاح");
                                    return new CustomReponse<AppUser> { StatusCode = 200, Data = user, Message = message };
                                }

                            }
                        }
                        else
                        {

                            message.Add("مشرف غير موجود");
                            return new CustomReponse<AppUser> { StatusCode = 404, Data = user, Message = message };
                        }

                    }

                    else
                    {

                        message.Add("مشرف غير موجود");
                        return new CustomReponse<AppUser> { StatusCode = 404, Data = user, Message = message };
                    }


                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                return new CustomReponse<AppUser> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (Exception ex)
            {
                message.Add(ex.InnerException.Message);
                return new CustomReponse<AppUser> { StatusCode = 500, Message = message, Data = null };

            }
        }


        [HttpPut]
        [Route("UpdateUser")]

        public async Task<CustomReponse<AppUser>> UpdateUser([FromBody] UpdateUserDTO App)
        {
            var message = new List<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    var userdata = dataContext.Users.Where(a => a.Id == App.Id).AsNoTracking().FirstOrDefault();
                    var user = dataContext.Users.Where(a => a.Email == App.Email).AsNoTracking().FirstOrDefault();
                    var user2 = dataContext.Users.Where(a => a.DisplayName == App.UserName).AsNoTracking().FirstOrDefault();
                    if (userdata is not null)
                    {
                        if (user is not null)
                        {
                            if (user.Id != App.Id)
                            {
                                message.Add("البريد الالكترونى موجود بالفعل");
                                return new CustomReponse<AppUser> { StatusCode = 400, Data = user, Message = message };
                            }

                            else
                            {

                                userdata.Email = App.Email;
                                userdata.PhoneNumber = App.Phone;
                                userdata.DisplayName = App.UserName;
                                userdata.UserName = App.UserName + GenereateToken().ToString();
                                userdata.PasswordHash = hasher.HashPassword(userdata, App.Password);

                                dataContext.ChangeTracker.Clear();
                                dataContext.Users.Update(userdata);
                                dataContext.SaveChanges();

                                message.Add("تم تعديل الطالب بنجاح");
                                return new CustomReponse<AppUser> { StatusCode = 200, Data = user, Message = message };

                            }
                        }
                        else
                        {

                            message.Add("'طالب غير موجود");
                            return new CustomReponse<AppUser> { StatusCode = 404, Data = user, Message = message };
                        }


                    }


                    else
                    {

                        message.Add("طالب غير موجود");
                        return new CustomReponse<AppUser> { StatusCode = 404, Data = user, Message = message };
                    }


                }
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                return new CustomReponse<AppUser> { StatusCode = 400, Data = null, Message = errors };


            }
            catch (Exception ex)
            {
                message.Add(ex.InnerException.Message);
                return new CustomReponse<AppUser> { StatusCode = 500, Message = message, Data = null };

            }
        }


        [HttpGet]
        [Route("GetAllAdmins")]
        public async Task<CustomReponse<IEnumerable<AppUser>>> GetAllAdmins()
        {
            var admins = new List<AppUser>();
            var data = usermanager.Users.AsNoTracking().ToList();
            foreach (var item in data)
            {
                var res = await usermanager.IsInRoleAsync(item, "Admin");
                if (res)
                {
                    admins.Add(item);

                }
            }
            var message = new string[]
            {
                "تم استرجاع بيانات المشرفين"
            }.ToList();
            return new CustomReponse<IEnumerable<AppUser>> { StatusCode = 400, Data = admins, Message = message };

        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<CustomReponse<AppUser>> GetById(string id)
        {
            var data = usermanager.Users.Where(a => a.Id == id).AsNoTracking().ToList().ElementAt(0);

            var message = new string[]
            {
                "  تم استرجاع البيانات بنجاح "
            }.ToList();
            return new CustomReponse<AppUser> { StatusCode = 200, Data = data, Message = message };

        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<CustomReponse<AppUser>> Delete(string id)
        {
            dataContext.Users.Remove(dataContext.Users.Find(id));
            dataContext.SaveChanges();

            var message = new string[]
            {
                "  تم الحذف بنجاح "
            }.ToList();
            return new CustomReponse<AppUser> { StatusCode = 200, Data = null, Message = message };

        }




       


        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<CustomReponse<IEnumerable<AppUser>>> GetAllUsers()
        {
            var Users = new List<AppUser>();
            var data = usermanager.Users.AsNoTracking().ToList();
            foreach (var item in data)
            {
                var res = await usermanager.IsInRoleAsync(item, "User");
                if (res)
                {
                    Users.Add(item);

                }
            }
            var message = new string[]
            {
                "تم استرجاع بيانات المستخدمين"
            }.ToList();
            return new CustomReponse<IEnumerable<AppUser>> { StatusCode = 400, Data = Users, Message = message };

        }



        private int GenereateToken()
        {
            var data = "1234567890";
            var result = "";
            for (int i = 0; i < 5; i++)
            {
                Random Random = new Random();
                result += data[Random.Next(0, 10)];



            }
            return int.Parse(result);
        }


      
        

        //        [HttpGet]
        //        [Route("Getdata")]
        //        public IActionResult getdata()
        //        {
        //            var data = dataContext.AcademicYears.Include(a => a.AcademicYearCourses).ThenInclude(a => a.AcademicYearCoursesTeachers).ThenInclude(a => a.Teacher).Include(a => a.AcademicYearCourses).ThenInclude(a => a.Course);
        //            var res = data.Select(a => new AcademicYearDetails
        //            {
        //                AcademicYearId = a.Id,
        //                AcademicYearName = a.Name,
        //                Courses = a.AcademicYearCourses.Select(crs => crs.Course).Distinct().Select(crsdto => new CourseWithTeacherDTO
        //                {

        //                    Id = crsdto.Id,
        //                    Name = crsdto.Name,
        //                    Description = crsdto.Description,
        //                    teachers =( a.AcademicYearCourses.Where(bb => bb.AcademicYearId == a.Id && bb.CourseId == crsdto.Id).Select(ayt => ayt.AcademicYearCoursesTeachers.Select(aytd => new TeacherDTO
        //                    {
        //                        Id = aytd.TeacherId,

        //                        Email = aytd.Teacher.Email,
        //                        Name = aytd.Teacher.Name,
        //                        Phone = aytd.Teacher.Phone,
        //                       startDate = aytd.startDate,
        //                       endDate = aytd.endDate,
        //                       NumberOfLessons=aytd.NumberOfLessons,
        //                       YoutubeLink= aytd.YoutubeLink,

        //                    }))).FirstOrDefault()



        //                    //{
        //                    //    Id = aytd.,
        //                    //    Email = aytd.Select(a => a.Teacher).FirstOrDefault().Email,
        //                    //    Name = aytd.Select(a => a.Teacher).FirstOrDefault().Name,
        //                    //    Phone = aytd.Select(a => a.Teacher).FirstOrDefault().Email,
        //                    //    startDate = a.AcademicYearCourses.Where(nbnvn => nbnvn.AcademicYearId == a.Id && nbnvn.CourseId == crsdto.Id).FirstOrDefault().AcademicYearCoursesTeachers.Where(a => a.TeacherId == aytd.Select(a => a.Teacher).FirstOrDefault().Id).FirstOrDefault().startDate,
        //                    //    endDate = a.AcademicYearCourses.Where(nbnvn => nbnvn.AcademicYearId == a.Id && nbnvn.CourseId == crsdto.Id).FirstOrDefault().AcademicYearCoursesTeachers.Where(a => a.TeacherId == aytd.Select(a => a.Teacher).FirstOrDefault().Id).FirstOrDefault().endDate

        //                    //})



        //                    //.Select(aytd => new TeacherDTO
        //                    //{
        //                    //    Id = aytd.Teacher.Id,
        //                    //    Email = aytd.Teacher.Email,
        //                    //    Name = aytd.Teacher.Name,
        //                    //    Phone = aytd.Teacher.Phone,
        //                    //    startDate = a.AcademicYearCourses.Where(nbnvn => nbnvn.AcademicYearId == a.Id && nbnvn.CourseId == crsdto.Id).FirstOrDefault().AcademicYearCoursesTeachers.Where(a => a.TeacherId == aytd.TeacherId).FirstOrDefault().startDate,
        //                    //    endDate = a.AcademicYearCourses.Where(nbnvn => nbnvn.AcademicYearId == a.Id && nbnvn.CourseId == crsdto.Id).FirstOrDefault().AcademicYearCoursesTeachers.Where(a => a.TeacherId == aytd.TeacherId).FirstOrDefault().endDate

        //                    //})),


        //                })


        //            }
        //            ); ; ; ; ; ; ;
        //            return Ok(res);

        //}


    }



}

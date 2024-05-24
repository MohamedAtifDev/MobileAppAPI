using AutoMapper;
using GraduationProjectAPI.BL.Services;
using GraduationProjectAPI.BL.VM;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MobileApp.BL.CustomReponse;

using MobileApp.BL.DTO;
using MobileApp.BL.VM;
using MobileApp.DAL.DataContext;
using MobileApp.DAL.Entities;
using Newtonsoft.Json.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using MobileApp.BL.VM;
using Microsoft.EntityFrameworkCore;
using MobileApp;
using System.Runtime.CompilerServices;


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
        

        public AccountController(DataContext dataContext,UserManager<AppUser> usermanager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager,IMapper mapper,IPasswordHasher<AppUser> hasher)
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
                        UserName = sign.UserName,
                        PhoneNumber=sign.Phone,
                       

                        

                    };


                    var data = await usermanager.FindByEmailAsync(sign.Email);
                    if (data != null)
                    {
                        message.Add("البريد الالكترونى مسجل بالفعل");
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = message };
                    }
                    var data2 = await usermanager.FindByNameAsync(sign.UserName);
                    if (data2 != null)
                    {
                        message.Add("اسم المستخدم مسجل بالفعل");
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = message };
                    }
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
                                    UserName = user.Email,
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


        [HttpPost]
        [Route("UserSignUp")]
        public async Task<CustomReponse<UserDTO>> UserSignUp([FromBody] SignUpDTO sign)
        { 
            try
            {
                var message = new List<string>();
                if (ModelState.IsValid)
                {
                   var data=await usermanager.FindByEmailAsync(sign.Email);
                    if (data != null)
                    {
                        message.Add("البريد الالكترونى مسجل بالفعل");
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = message };
                    }
                    var data2 = await usermanager.FindByNameAsync(sign.userName);
                    if (data2 != null)
                    {
                        message.Add("اسم المستخدم مسجل بالفعل");
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = message };
                    }
                    var user = new AppUser
                    {
                        Email = sign.Email,
                        UserName = sign.userName,

                    };
                  

                        var result = await usermanager.CreateAsync(user, sign.Password);

                        if (result.Succeeded)
                        {
                            var role = await roleManager.FindByIdAsync("1");
                            var AddToRole = await usermanager.AddToRoleAsync(user, role.Name);

                            if (AddToRole.Succeeded)
                            {
                                message.Add("تم الاشتراك بنجاح");


                                var userdata = new UserDTO
                                {
                                    Email = user.Email,
                                    UserName = user.UserName,
                                    RoleName = "User"
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



        [HttpPost]
        [Route("UserSignIn")]
        public async Task<CustomReponse<UserDTO>> UserSignIn([FromBody] SignInDTO sign)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await usermanager.FindByEmailAsync(sign.Email);
                    var result = await signInManager.PasswordSignInAsync(user.UserName, sign.Password, sign.remember == null ? false :true, false);
             
                 
                   

                    if (result.Succeeded)
                    {
                        var IsInRole = await usermanager.IsInRoleAsync(user, "User");

                        if (IsInRole)
                        {
                            var messages = new string[]
                      {
                               "تم تسجيل الدخول بنجاح"
                      }.ToList();

                            var userdata = new UserDTO
                            {
                                Email = user.Email,
                                RoleName = "User"
                            };
                            return new CustomReponse<UserDTO> { StatusCode = 200, Message = messages, Data = userdata };
                        }
                        else
                        {
                            var invalidmessages = new string[]
                                               {
                               "كلمه المرور او البريد الالكترونى غير صحيح"
                                               }.ToList();
                            return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data = null};
                        }
                      
                    }
                    else
                    {
                        var invalidmessages = new string[]
                       {
                                    "كلمه المرور او البريد الالكترونى غير صحيح"
                       }.ToList();
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data =null };

                    }

                }

                var message = new List<string>(); ;
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
                return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = null };
            }


        }

        [HttpPost]
        [Route("AdminSignIn")]
  
        public async Task<CustomReponse<UserDTO>> AdminSignIn([FromBody] SignInDTO sign)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user =await  usermanager.FindByEmailAsync(sign.Email);
                    var result = await signInManager.PasswordSignInAsync(user.UserName, sign.Password, sign.remember==null ?false :true, false);

              


                    if (result.Succeeded)
                    {
                        var IsInRole = await usermanager.IsInRoleAsync(user, "Admin")|| await usermanager.IsInRoleAsync(user, "SuperAdmin");

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
                            return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data = null};
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

        public async Task<CustomReponse<AppUser>> UpdateAdmin([FromBody]UpdateUserDTO App)
        {
            var message = new List<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    var userdata = dataContext.Users.Where(a => a.Id == App.Id).AsNoTracking().FirstOrDefault();
                    var user = dataContext.Users.Where(a => a.Email == App.Email).AsNoTracking().FirstOrDefault();
                    var user2= dataContext.Users.Where(a => a.UserName == App.UserName).AsNoTracking().FirstOrDefault();
                    if (userdata is not null)
                    {
                        if (user is not null)
                        {
                            if (user.Id != App.Id)
                            {
                                message.Add("البريد الالكترونى موجود بالفعل");
                                return new CustomReponse<AppUser> { StatusCode = 400, Data = user, Message = message };
                            }
                        }

                        else if (user2 is not null && user2.Id!= App.Id)
                        {
                            message.Add("اسم المستخدم موجود بالفعل");
                            return new CustomReponse<AppUser> { StatusCode = 400, Data = user, Message = message };
                        }
                        else
                        {
                          var checker=new CustomPasswordValidator<AppUser>();
                             var result=await checker.ValidateAsync(usermanager, userdata, App.Password);
                            if (result != null)
                            {
                                foreach (var item in result.Errors)
                                {
                                    message.Add(item.Description);
                                }
                                return new CustomReponse<AppUser> { StatusCode = 400, Data = user, Message = message };

                            }
                            userdata.Email = App.Email;
                            userdata.PhoneNumber = App.Phone;
                            userdata.UserName = App.UserName;
                            userdata.PasswordHash = hasher.HashPassword(userdata, App.ConfirmPassword);
                        
                            dataContext.ChangeTracker.Clear();
                            dataContext.Users.Update(userdata);
                            dataContext.SaveChanges();

                            message.Add("تم تعديل المشرف بنجاح");
                            return new CustomReponse<AppUser> { StatusCode = 200, Data = user, Message = message };

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
            return new CustomReponse<AppUser>{ StatusCode = 200, Data = data, Message = message };

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
                Random Random=new Random();
                result+=data[Random.Next(0, 10)];


                
            }
            return int.Parse(result);
        }



    }
}

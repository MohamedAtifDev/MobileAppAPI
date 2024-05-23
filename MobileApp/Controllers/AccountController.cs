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


namespace OnlineExamAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [EnableCors]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> usermanager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<AppUser> hasher;

        public AccountController(UserManager<AppUser> usermanager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager,IMapper mapper,IPasswordHasher<AppUser> hasher)
        {
            this.usermanager = usermanager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.hasher = hasher;
            this.roleManager = roleManager;
        }

        //[HttpGet]
        //[Route("~/Account/getuser/{id}")]
        //public async Task<Response<AppUser>> getuser(string id)
        //{
        //    var user = await usermanager.FindByIdAsync(id);
        //  if(user == null)
        //    {
        //        return new Response<AppUser> { statusCode = 200, message = "No user", result = null };

        //    }
        //    else
        //    {
        //        return new Response<AppUser> { statusCode = 200, message = "User is exist", result = user };
        //    }
        //}
        [HttpPost]
        [Route("AdminSignUp")]
        public async Task<CustomReponse<UserDTO>> AdminSignUp([FromBody] SignUpDTO sign)
        {
            try
            {
                var message = new List<string>();
                if (ModelState.IsValid)
                {
                    var user = new AppUser
                    {
                        Email = sign.Email,
                        UserName = sign.userName,

                    };
                    var result = await usermanager.CreateAsync(user, sign.Password);

                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync("2");
                        var AddToRole = await usermanager.AddToRoleAsync(user, role.Name);

                        if (AddToRole.Succeeded)
                        {
                            message.Add("sign up successfully");


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
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = user as UserDTO };

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
                    var user = new AppUser
                    {
                        Email = sign.Email,
                        UserName = sign.userName,

                    };
                    var result = await usermanager.CreateAsync(user,sign.Password);

                    if (result.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync("1");
                        var AddToRole = await usermanager.AddToRoleAsync(user,role.Name);

                        if (AddToRole.Succeeded)
                        {
                            message.Add("sign up successfully");
                   

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
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = message, Data = user as UserDTO };

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
                    var result = await signInManager.PasswordSignInAsync(sign.UserName, sign.Password, sign.remember, false);
             
                    var user=await usermanager.FindByNameAsync(sign.UserName);
                   

                    if (result.Succeeded)
                    {
                        var IsInRole = await usermanager.IsInRoleAsync(user, "User");

                        if (IsInRole)
                        {
                            var messages = new string[]
                      {
                               "sign in successfully"
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
                               "Invalid UserName or Password Attempt"
                                               }.ToList();
                            return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data = user as UserDTO };
                        }
                      
                    }
                    else
                    {
                        var invalidmessages = new string[]
                       {
                               "Invalid UserName or Password Attempt"
                       }.ToList();
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data = user as UserDTO };

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
                    var result = await signInManager.PasswordSignInAsync(sign.UserName, sign.Password, sign.remember, false);

                    var user = await usermanager.FindByNameAsync(sign.UserName);


                    if (result.Succeeded)
                    {
                        var IsInRole = await usermanager.IsInRoleAsync(user, "Admin")|| await usermanager.IsInRoleAsync(user, "SuperAdmin");

                        if (IsInRole)
                        {
                            var messages = new string[]
                      {
                               "sign in successfully"
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
                               "Invalid UserName or Password Attempt"
                                               }.ToList();
                            return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data = user as UserDTO };
                        }

                    }
                    else
                    {
                        var invalidmessages = new string[]
                       {
                               "Invalid UserName or Password Attempt"
                       }.ToList();
                        return new CustomReponse<UserDTO> { StatusCode = 400, Message = invalidmessages, Data = user as UserDTO };

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
                    var email = $"Your Validation Code is {token} ";

                    var state = MailSender.sendmail(fg.Email, email);
                    var message = new List<string>();
                    if (state.Result)
                    {
                    
                        message.Add("Token  sended Successfully");
                        return new CustomReponse<string> { StatusCode = 200, Message = message, Data = token.ToString() };
                    }
                    else
                    {
                        message.Add("Something Wrong");
                        return new CustomReponse<string> { StatusCode = 500, Message = message, Data = null };
                    }


                }
                else
                {

                    var message = new List<string>();
                    message.Add("User Not Found");
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
                            message.Add("Password Rested Successfully");
                            return new CustomReponse<string> { StatusCode = 200, Message = message, Data = null };
                        }
                        else
                        {
                            message.Add("Something Wrong");
                            return new CustomReponse<string> { StatusCode = 500, Message = message, Data = null };
                        }
                    }
                    else
                    {
                        message.Add("Invalid Token");
                        return new CustomReponse<string> { StatusCode = 500, Message = message, Data = null };
                    }
                }
                else
                {

                    message.Add("User Not Found");
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

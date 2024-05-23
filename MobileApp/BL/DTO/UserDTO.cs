using Microsoft.AspNetCore.Identity;
using MobileApp.DAL.Entities;

namespace MobileApp.BL.DTO
{
    public class UserDTO:AppUser
    {
        public string RoleName {  get; set; }

     
    }
}

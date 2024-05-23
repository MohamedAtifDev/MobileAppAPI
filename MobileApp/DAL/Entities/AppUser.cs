using Microsoft.AspNetCore.Identity;

namespace MobileApp.DAL.Entities
{
    public class AppUser:IdentityUser
    {
        public int Token {  get; set; }
    }
}

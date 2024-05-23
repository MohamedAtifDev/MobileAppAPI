using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MobileApp;
public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
{
    public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
    {
        var errors = new List<IdentityError>();

        // Example: Custom password length validation
        if (password.Length < 8)
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordTooShort",
                Description = "كلمة المرور يجب الا تقل عن 8 حروف"
            });
        }

        // Example: Custom digit validation
        if (!password.Any(char.IsDigit))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordRequiresDigit",
                Description = "كلمة المرور يجب ان تحتوي على الاقل على رقم "
            });
        }
        if (!password.Any(c=>!char.IsLetterOrDigit(c)))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordRequiresDigit",
                Description = " @ كلمة المرور يجب ان تحتوي على الاقل عللى رمز خاص مثل "
            });
        }

        // Add more custom validations as needed

        if (errors.Any())
        {
            return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }

        return Task.FromResult(IdentityResult.Success);
    }
}


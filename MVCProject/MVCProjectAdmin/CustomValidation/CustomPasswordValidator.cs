using Microsoft.AspNetCore.Identity;
using MVCProjectDAL.Model.Identity;

namespace MVCProjectAdmin.CustomValidation
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (password.ToLower().Contains("qwe"))
            {
                IdentityError error = new IdentityError();
                error.Code = "CommonTextPassword";
                error.Description = "Common text cant user for password";
                errors.Add(error);
            }
            if (password.ToLower().Contains("123"))
            {
                IdentityError error = new IdentityError();
                error.Code = "CommonNuberPassword";
                error.Description = "Common text cant user for password";
                errors.Add(error);
            }

            if (errors.Count() == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

        }
    }
}

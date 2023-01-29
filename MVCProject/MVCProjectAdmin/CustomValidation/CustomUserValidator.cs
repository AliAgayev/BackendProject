using Microsoft.AspNetCore.Identity;
using MVCProjectDAL.Model.Identity;

namespace MVCProjectAdmin.CustomValidation
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> userErrors = new List<IdentityError>();
            if (user.Email.Length > 25)
            {
                IdentityError error = new IdentityError();
                error.Code = "EmailLength";
                error.Description = "Email uzunlugu 15 simvoldan uzun ola bilmez";
                userErrors.Add(error);
            }
            if (userErrors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(userErrors.ToArray()));
            }
        }
    }
}

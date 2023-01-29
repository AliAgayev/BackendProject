using Microsoft.AspNetCore.Identity;

namespace MVCProjectAdmin.CustomDescriber
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            IdentityError error = new IdentityError();


            error.Code = "PasswordTooShort";
            error.Description = $"Şifrənin uzunluğu minimum {length} olmalıdır";
            return error;
        }
    }
}

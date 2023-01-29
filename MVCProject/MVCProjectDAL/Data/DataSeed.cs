using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCProjectDAL.Model.Identity;

namespace MVCProjectDAL.Data
{
    public static class DataSeed
    {
        public static IApplicationBuilder Seed(this IApplicationBuilder app)
        {
            const string adminEmail = "tetgagayev8@gmail.com";
            const string adminPassword = "Agayev.2000";
            const string superAdminRoleName = "SuperAdmin";

            using (var scope = app.ApplicationServices.CreateScope())
            {

                var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                db.Database.Migrate();

                var role = roleManager.FindByNameAsync(superAdminRoleName).Result;
                if (role == null)
                {
                    role = new AppRole
                    {
                        Name = superAdminRoleName
                    };

                    roleManager.CreateAsync(role).Wait();
                }


                var userManeger = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var adminUser = userManeger.FindByEmailAsync(adminEmail).Result;

                if (adminUser == null)
                {
                    adminUser = new AppUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        EmailConfirmed = true
                    };

                    var userResult = userManeger.CreateAsync(adminUser, adminPassword).Result;

                    if (userResult.Succeeded)
                    {
                        userManeger.AddToRoleAsync(adminUser, superAdminRoleName).Wait();
                    }

                }
            }
            return app;
        }
    }
}

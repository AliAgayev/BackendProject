using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProjectDAL.Data;
using MVCProjectDAL.Enum;
using MVCProjectDAL.Model;
using MVCProjectDAL.Model.Identity;
using MVCProjectUI.Models.ViewModel;

namespace MVCProjectUI.Controllers
{
    public class AccountController : Controller
    {
        readonly SignInManager<AppUser> signInManager;
        readonly UserManager<AppUser> userManager;
        readonly AppDBContext _context;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userInManager, AppDBContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userInManager;
            _context = context;
        }


        [AllowAnonymous]
        public IActionResult Signin()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        async public Task<IActionResult> Signin(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appUser = await userManager.FindByNameAsync(model.UserName);
                if (appUser == null)
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    return View(model);
                }
                var result = await signInManager.PasswordSignInAsync(appUser, model.Password, true, true);

                if (result.Succeeded)
                {

                    string redirect = Request.Query["returnUrl"];
                    if (string.IsNullOrWhiteSpace(redirect))
                        return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    return View(model);
                }
            }
        }
        async public Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Signin");
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        async public Task<IActionResult> Register (SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {

                Customer customer = new Customer()
                {
                    Address = model.Address,
                    Name = model.Name,
                    Surname = model.Surname,

                };
                _context.Customers.Add(customer);
                _context.SaveChanges();

                var appUser = new AppUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    UserType = (int)UserType.Customer,
                    CustomerId = customer.Id
                };
                var result = await userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {

                    return RedirectToAction(nameof(Signin));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProjectAdmin.Models.ViewModels;
using MVCProjectDAL.Data;
using MVCProjectDAL.Enum;
using MVCProjectDAL.Model;
using MVCProjectDAL.Model.Identity;
using System.Collections.Generic;
using System.Data;

namespace MVCProjectAdmin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Manager")]

    public class UserController : Controller
    {
        private readonly AppDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public UserController(AppDBContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult EmplyoeeIndex()
        {


            List<UserManagerViewModel> userViewModels = new List<UserManagerViewModel>();

            IEnumerable<AppUser> appUsers = _context.Users.Where(e => e.UserType == (int)UserType.Operator);
            foreach (var item in appUsers)
            {
                UserManagerViewModel userManagment = new UserManagerViewModel()
                {
                    UserName = item.UserName,
                    Email = item.Email,
                    UserType = item.UserType,
                    UserId = item.Id



                };
                Employee employee = _context.Employees.Where(e => e.Id == item.EmployeeId).FirstOrDefault();
                if (employee != null)
                {
                    userManagment.FullName = employee.Name + "  " + employee.Surname;
                }
                userViewModels.Add(userManagment);

            }
            return View(userViewModels);
        }
        public IActionResult CustomerIndex()
        {


            List<UserManagerViewModel> userViewModels = new List<UserManagerViewModel>();

            IEnumerable<AppUser> appUsers = _context.Users.Where(e => e.UserType == (int)UserType.Customer);
            foreach (var item in appUsers)
            {
                UserManagerViewModel userManagment = new UserManagerViewModel()
                {
                    UserName = item.UserName,
                    Email = item.Email,
                    UserType = item.UserType,
                    UserId = item.Id



                };
                Customer customer = _context.Customers.Where(e => e.Id == item.CustomerId).FirstOrDefault();
                if (customer != null)
                {
                    userManagment.FullName = customer.Name + " " + customer.Surname;
                }
                userViewModels.Add(userManagment);

            }
            return View(userViewModels);
        }


        public IActionResult EmployeeUserCreate()
        {
            IEnumerable<Employee> empList = _context.Employees;
            List<SelectListItem> employess = ConvertEmployetoListItem(empList);
            UserManagerViewModel viewModel = new UserManagerViewModel();
            viewModel.Employees = employess;

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeUserCreate(UserManagerViewModel viewModel)
        {
            try
            {
                AppUser appUser = new AppUser()
                {
                    UserName = viewModel.UserName,
                    Email = viewModel.Email,
                    EmployeeId = viewModel.EmployeeId,
                    UserType = (int)UserType.Operator

                };
                await _userManager.CreateAsync(appUser, viewModel.Password);

                return RedirectToAction("EmplyoeeIndex");
            }
            catch (Exception)
            {

                return View(viewModel);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            UserManagerViewModel viewModel = new UserManagerViewModel();
            if (id == null || viewModel == null)
            {
                return NotFound();
            }

            var view = await _context.Users
         .FirstOrDefaultAsync(m => m.Id == id);
            if (view == null)
            {
                return NotFound();
            }

            return View(view);
        }

        public IActionResult Delete(int? id)
        {
            AppUser user = _context.Users.Where(p => p.Id == id).FirstOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("EmplyoeeIndex");
        }


        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                IEnumerable<Employee> empList = _context.Employees;
                List<SelectListItem> employess = ConvertEmployetoListItem(empList);
                UserManagerViewModel userViewModels = new UserManagerViewModel()
                {

                    UserName = user.UserName,
                    Email = user.Email,
                    UserType = user.UserType,
                    UserId = user.Id,
                    EmployeeId = user.EmployeeId == null ? 0 : (int)user.EmployeeId,

                };
                userViewModels.Employees = employess;
                return View(userViewModels);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserManagerViewModel viewModel)
        {


            try
            {
                AppUser appUser = await _userManager.FindByIdAsync(id.ToString());

                appUser.Email = viewModel.Email;
                appUser.UserName = viewModel.UserName;
                appUser.EmployeeId = viewModel.EmployeeId;

                await _userManager.UpdateAsync(appUser);

                if (appUser.UserType == (int)UserType.Operator)
                {
                    return RedirectToAction("EmplyoeeIndex");
                }
                else
                {
                    return RedirectToAction("CustomerIndex");
                }

            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return View(viewModel);
        }


        public List<SelectListItem> ConvertEmployetoListItem(IEnumerable<Employee> list)
        {

            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var item in list)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name + " - " + item.Surname

                };
                listItems.Add(listItem);
            }
            return listItems;

        }
        public List<SelectListItem> ConvertCustomertoListItem(IEnumerable<Customer> list)
        {

            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var item in list)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name + " - " + item.Surname

                };
                listItems.Add(listItem);
            }
            return listItems;

        }
        public List<SelectListItem> ConvertRoletoListItem(IEnumerable<AppRole> list)
        {

            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var item in list)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = item.Name,
                    Text = item.Name

                };
                listItems.Add(listItem);
            }
            return listItems;

        }
        public async Task<IActionResult> RoleAssign(int? id)
        {
            UserRoleViewModel viewModel = new UserRoleViewModel();
            ViewBag.Role = new SelectList(_context.Roles.ToList(), "Id", "Name");
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                Employee currentEmployee = await _context.Employees.FindAsync(user.EmployeeId);
                if (currentEmployee != null)
                {
                    viewModel = new UserRoleViewModel()
                    {
                        UserId = user.Id,
                        EmployeeId = currentEmployee.Id,
                        EmpFullName = currentEmployee.Name + " " + currentEmployee.Surname,



                    };


                    IEnumerable<AppRole> roleList = _roleManager.Roles;
                    List<SelectListItem> selectListItems = ConvertRoletoListItem(roleList);

                    viewModel.Roles = selectListItems;
                    return View(viewModel);

                }


            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleAssign(UserRoleViewModel viewModel)
        {

            bool isExist = await _roleManager.RoleExistsAsync(viewModel.RoleName);


            if (isExist)
            {
                AppUser appUser = await _userManager.FindByIdAsync(viewModel.UserId.ToString());
                await _userManager.AddToRoleAsync(appUser, viewModel.RoleName);
                ViewBag.Role = new SelectList(_context.Roles.ToList(), "Id", "Name");
                return RedirectToAction("EmplyoeeIndex");
            }


            return View(viewModel);
        }
    }
}


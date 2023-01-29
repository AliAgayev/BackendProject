using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCProjectAdmin.Models.ViewModels
{
    public class UserRoleViewModel : Controller
    {
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string EmpFullName { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}

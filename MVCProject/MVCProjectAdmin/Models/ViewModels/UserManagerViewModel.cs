using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVCProjectAdmin.Models.ViewModels
{
    public class UserManagerViewModel : Controller
    {
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string FullName { get; set; }
        public int UserType { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public int EmployeeId { get; set; }
        public List<SelectListItem> Employees { get; set; }
        public List<SelectListItem> Customers { get; set; }
    }
}

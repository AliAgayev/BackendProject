using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCProjectUI.Models.ViewModel
{
    public class SignUpViewModel
    {
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("E-mail")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DisplayName("Telephone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Address { get; internal set; }
    }
}
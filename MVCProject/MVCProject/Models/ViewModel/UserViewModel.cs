using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCProjectUI.Models.ViewModel
{
    public class UserViewModel
    {
        [Required]
        [DisplayName("İstifadəçi adı")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email ünvanı")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Şifrə")]
        public string Password { get; set; }
    }
}
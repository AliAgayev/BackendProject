using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCProjectUI.Models.ViewModel
{
    public class SingInViewModel
    {
        [DisplayName("Istifadəçi adı")]
        public string UserName { get; set; }

        [DisplayName("Şifrə")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
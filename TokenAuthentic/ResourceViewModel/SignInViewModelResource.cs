using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TokenAuthentic.ResourceViewModel
{
    public class SignInViewModelResource
    {
        [Required(ErrorMessage = "İstifadəçi adı yazılmalıdır")]
        [EmailAddress(ErrorMessage = "E-poçt ünvanınız düzgün formatda deyil")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Şifrə yazılmalıdır")]
        [DataType(DataType.Password)]
        [MinLength(4,ErrorMessage ="Şifrə ən azı 4 xarakterli olmalıdır")]

        public string Password { get; set; }

        public string RememberMe { get; set; }
    }
}

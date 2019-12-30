using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TokenAuthentic.Enum;

namespace TokenAuthentic.ResourceViewModel
{
    public class UserViewModelResource
    {
        [Required(ErrorMessage ="İstifadəçi adı yazılmalıdır")]

        public string UserName { get; set; }
        [RegularExpression(@"^(0{\d{3}} {\d{3}} {\d{2}} {\d{2}})$",ErrorMessage ="Telefon nömrəsini daxil edin")]

        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "İstifadəçi adı yazılmalıdır")]
        [EmailAddress(ErrorMessage ="E-poçt ünvanınız düzgün formatda deyil")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Şifrə yazılmalıdır")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        public DateTime? Birthday { get; set; }

        public string Picture { get; set; }

        public string City { get; set; }

        public Gender Gender { get; set; }
    }
}

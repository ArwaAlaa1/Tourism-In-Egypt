using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Helper.DTO
{
    public class ChangPasswordDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password Is required")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password Doesn't match password")]
        public string ConfirmPass { get; set; }

        [Required(ErrorMessage = "New Password Is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}

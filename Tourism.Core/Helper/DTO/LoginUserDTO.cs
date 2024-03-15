using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Helper.DTO
{
    public class LoginUserDTO
    {
        
        public string? Email { get; set; }
        public string? Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

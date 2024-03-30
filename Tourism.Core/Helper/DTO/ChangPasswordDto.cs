using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Helper.DTO
{
    public class ChangPasswordDto
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}

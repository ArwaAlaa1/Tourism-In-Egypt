using Org.BouncyCastle.Utilities.Date;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Entities
{
    public class ResetPassword: BaseEntity
    {

        public string Email { get; set; }

        public string Token { get; set; }
        public int OTP { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}

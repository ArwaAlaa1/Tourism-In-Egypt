using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Helper.DTO
{
    public class TripDTO
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string? ImgUrl { get; set; }
        public DateTime StartDate { get; set; }
      
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}

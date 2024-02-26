using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Helper;

namespace Tourism.Core.Entities
{
    public class CityPhotos : BaseEntity
    {
        //[UniqueCityPhoto]
        public int CityId { get; set; }
        public string Photo { get; set; }
        public virtual City city { get; set; }
    }
}

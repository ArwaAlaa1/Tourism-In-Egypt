using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Entities
{
    public  class City:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public virtual IEnumerable<CityPhotos> CityPhotos { get; set; } 

        public virtual IEnumerable<Place> Places { get; set; } 



    }
}

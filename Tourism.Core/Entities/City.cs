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

        public virtual ICollection<CityPhotos> CityPhotos { get; set; } = new List<CityPhotos>();

        public virtual ICollection<Place> Places { get; set; } = new List<Place>();



    }
}

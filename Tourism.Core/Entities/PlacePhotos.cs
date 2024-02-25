using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Entities
{
    public class PlacePhotos : BaseEntity
    {
        public int PlaceId { get; set; }
        public string Photo { get; set; }

        public virtual  Place Place { get; set; }

    }
}

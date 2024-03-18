using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Core.Helper.DTO
{
    public class PlaceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
        public string Link { get; set; }
        public string Phone { get; set; }

        public string Category { get; set; }
        public string City { get; set; }
      
        
        public virtual IEnumerable<PhotoDTO> Photos { get; set; } 
    }
}

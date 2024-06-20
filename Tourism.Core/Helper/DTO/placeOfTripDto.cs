using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Helper.DTO
{
    public class placeOfTripDto
    {
        public int Id { get; set; }
        public string placeName { get; set; }

        
        public string Location { get; set; }
		public virtual IEnumerable<PhotoDTO> Photos { get; set; }

	}
}

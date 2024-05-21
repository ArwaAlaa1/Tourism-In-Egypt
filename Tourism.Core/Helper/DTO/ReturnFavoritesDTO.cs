using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Core.Helper.DTO
{
    public class ReturnFavoritesDTO
    {
        public int FavoriteId { get; set; }
     
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
        public string Link { get; set; }
        public string city { get; set; }
        public bool IsFav { get; set; } 

        public IEnumerable<PhotoDTO> photos { get; set; }


    }
}

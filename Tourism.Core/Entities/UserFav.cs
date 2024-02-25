using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Entities
{
    public class UserFav : BaseEntity
    {   
        
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        
        public int PlaceId { get; set; }
    }
}

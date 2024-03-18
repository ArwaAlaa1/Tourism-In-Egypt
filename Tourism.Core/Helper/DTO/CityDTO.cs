﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Core.Helper.DTO
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public virtual IEnumerable<PhotoDTO> cityPhotos { get; set; }

        public virtual IEnumerable<PlaceDTO> Places { get; set; } 


       
    }
}

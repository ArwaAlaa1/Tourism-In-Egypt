﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Core.Entities
{
    public class Place:BaseEntity
    {

        public string  Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
        public string Link { get; set; }
        public string Phone { get; set; }

		//public IEnumerable<City> Cities_List { get; set; } = new List<City>();

		//public IEnumerable<Category> Categories_List { get; set; } = new List<Category>();

		public int CategoryId { get; set; } 
        public virtual Category Category { get; set; }

        public int CityId { get; set; } 
        public virtual City City { get; set; }

        public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
        public  virtual ICollection<User> Users { get; set; } = new List<User>();

        //public  ICollection<PlacePhotos> placePhotos { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourism.Core.Entities;

namespace Tourism.Core.Helper.DTO
{
    public class ReviewsPlaceDTOs
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }

        public string? Message { get; set; }

        public float Rating { get; set; }


        //public string? ImgURL { get; set; }
        public DateTime CreatedDate { get; set; } 

        public int UserId { get; set; }
        public int PlaceId { get; set; }

    }
}

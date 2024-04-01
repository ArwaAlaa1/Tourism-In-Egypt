using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Helper;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;
using AutoMapper;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Tourism_Egypt.Controllers
{
  
    public class PlaceController : BaseApiController
    {
        private readonly IPlaceRepository _placerepo;
        private readonly IMapper mapper;
       
        public PlaceController(IPlaceRepository placerepo,IMapper mapper)
        {
            _placerepo = placerepo;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceDTO>>> GetAllPlaces(string? placeName)
        {
            
            var places = await _placerepo.GetAllAsync();
            if (!string.IsNullOrEmpty(placeName))
            {
                var results = places.Where(e => e.Name.ToLower().Contains(placeName.ToLower())).ToList();

                if (results.Count() == 0)
                    return NotFound("This Place Not Existing");
                else
                {
                   var placesearch = mapper.Map<IEnumerable<Place>, IEnumerable<PlaceDTO>>(results);
                return Ok(placesearch);

                }

                
            }

      
            var data = mapper.Map<IEnumerable<Place>, IEnumerable<PlaceDTO>>(places);

            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceDTO>> GetPlace(int id)
        {
            var place = await _placerepo.GetAsync(id);

            if (place == null)
                return NotFound();

            //var photos = await _placerepo.GetAllPhotoBySpecIdAsync(id);
            //place.Photos= photos;
            var data = mapper.Map<Place, PlaceDTO>(place);

            return Ok(data);


        }
    }
}

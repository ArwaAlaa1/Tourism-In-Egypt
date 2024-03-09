using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism_Egypt.Controllers
{
   
    public class PlaceController : BaseApiController
    {
        private readonly IGenericRepository<Place> _placerepo;
        private readonly TourismContext _context;
        
        public PlaceController(IGenericRepository<Place> placerepo,TourismContext context)
        {
            _placerepo = placerepo;
           _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetAllPlaces()
        {
            var places = await _placerepo.GetAllAsync();

            return Ok(places);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> GetPlace(int id)
        {
            var place = await _placerepo.GetAsync(id);
            //var placeph = _context.PlacePhotos.Where(p => p.PlaceId == id).ToList();
            //place.placePhotos = placeph;

            //var photos = await _context.Places.Where(c => c.Id == id).Select(p => p.placePhotos).ToListAsync();
            //place.placePhotos = (ICollection<PlacePhotos>)photos.ToList();

            if (place == null)
                return NotFound();

            return Ok(place);
        }
    }
}

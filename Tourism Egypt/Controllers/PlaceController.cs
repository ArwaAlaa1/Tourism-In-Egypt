using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
   
    public class PlaceController : BaseApiController
    {
        private readonly IGenericRepository<Place> _placerepo;

        public PlaceController(IGenericRepository<Place> placerepo)
        {
            _placerepo = placerepo;
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

            if (place == null)
                return NotFound();

            return Ok(place);
        }
    }
}

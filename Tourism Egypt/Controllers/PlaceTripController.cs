using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class PlaceTripController : BaseApiController
    {
        private readonly IGenericRepository<Place_Trip> _placetrip;

        public PlaceTripController(IGenericRepository<Place_Trip> placetrip)
        {
            _placetrip = placetrip;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var List = await _placetrip.GetAllAsync();
            if (List == null)
            {
                return BadRequest();

            }
            else
                return Ok(List);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var PT = await _placetrip.GetAsync(id);

            if (PT == null)
            {
                return NotFound();
            }
            else
                return Ok(PT);
        }
    }
}

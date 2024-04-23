using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Authorize]
    public class TripController : BaseApiController
    {
        private readonly IGenericRepository<Trip> _trip;

        public TripController(IGenericRepository<Trip> trip)
        {
            _trip = trip;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowAll()
        {
            var List = await _trip.GetAllAsync();
            if (List == null)
            {
                return BadRequest();
            }
            else
                return Ok(List);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var trip = await _trip.GetAsync(id);

            if (trip == null)
            {
                return NotFound();
            }
            else
                return Ok(trip);
        }
    }
}

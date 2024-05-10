using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;
using System.ComponentModel.DataAnnotations;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    
    public class TripController : BaseApiController
    {
        private readonly IGenericRepository<Trip> _trip;
        private readonly IMapper _mapper;

        public TripController(IGenericRepository<Trip> trip
            ,IMapper mapper)
        {
            _trip = trip;
            _mapper = mapper;
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

        [HttpGet("SearchTrip")]
        public async Task<IActionResult> SearchTrip([Required]string _city , DateTime? _startDate , DateTime? _endDate)
        {

            var Trips = await _trip.GetAllAsync();

            if (_startDate == null || _endDate == null)
            {
                var trps = Trips.Where(x => x.City.ToLower().Contains(_city.ToLower())).ToList();
           var tripMapper = _mapper.Map<List<Trip> , List<TripDTO>>(trps);
                return Ok(tripMapper);
            }
            var trp = Trips.Where((x => x.City.ToLower().Contains(_city.ToLower()) && x.StartDate == _startDate &&
            x.EndDate == _endDate)).ToList();
            var tripMapper2 = _mapper.Map<List<Trip>, List<TripDTO>>(trp);

            return Ok(tripMapper2);
        }
    }
}

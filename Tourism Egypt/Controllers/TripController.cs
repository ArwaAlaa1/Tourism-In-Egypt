﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly IGenericRepository<Trip> _trip;

        public TripController(IGenericRepository<Trip> trip)
        {
            _trip = trip;
        }


        [HttpGet]
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

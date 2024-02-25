using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    
    public class CitiesController : BaseApiController
    {
        private readonly IGenericRepository<City> _cityRepo;

        public CitiesController(IGenericRepository<City> cityRepo)
        {
            _cityRepo = cityRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetAllCities()
        {
            var cities = await _cityRepo.GetAllAsync();

            return Ok(cities);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _cityRepo.GetAsync(id);

            if (city == null)
                return NotFound();

            return Ok(city);
        }
    }
}

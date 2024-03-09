using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism_Egypt.Controllers
{
    
    public class CityController : BaseApiController
    {
        private readonly TourismContext tourismContext;
        private readonly IGenericRepository<City> _cityRepo;
        private readonly IGenericRepository<CityPhotos> cityPhRepo;

        public CityController(TourismContext tourismContext,IGenericRepository<City> cityRepo,IGenericRepository<CityPhotos> cityPhRepo)
        {
            this.tourismContext = tourismContext;
            _cityRepo = cityRepo;
            this.cityPhRepo = cityPhRepo;
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
            //var photos = await tourismContext.Cities.Where(c => c.Id ==id).Select(p=>p.CityPhotos).ToListAsync();
            //city.CityPhotos = (ICollection<CityPhotos>)photos.ToList();
            if (city == null)
                return NotFound();

            return Ok(city);
        }
    }
}

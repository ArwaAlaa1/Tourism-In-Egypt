using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Helper;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism_Egypt.Controllers
{
   
    public class CityController : BaseApiController
    {

        private readonly ICityRepository _cityrepo;
       
        public CityController(ICityRepository cityrepo)
        {
            _cityrepo = cityrepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetAllCities()
        {
            var cities = await _cityrepo.GetAllAsync();

            return Ok(cities);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCity(int id)
        {
            var city = await _cityrepo.GetAsync(id);

            if (city == null)
                return NotFound();

            var photos =await _cityrepo.GetAllPhotoBySpecIdAsync(id);
     
            var mapped = new CityDTO()
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
                Location = city.Location,
                //cityPhotos = PhotosResolve.ConvertionCity(photos),  
            };
            return Ok(mapped);
        }
    }
}

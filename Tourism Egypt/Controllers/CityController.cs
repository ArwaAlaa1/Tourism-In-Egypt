using AutoMapper;
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
        private readonly IMapper mapper;

        public CityController(ICityRepository cityrepo,IMapper mapper)
        {
            _cityrepo = cityrepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetAllCities()
        {
            var cities = await _cityrepo.GetAllAsync();
            var data = mapper.Map<IEnumerable<City>, IEnumerable<CityDTO>>(cities);

            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCity(int id)
        {
            var city = await _cityrepo.GetAsync(id);

            if (city == null)
                return NotFound();

            //var photos =await _cityrepo.GetAllPhotoBySpecIdAsync(id);
            var data = mapper.Map<City, CityDTO>(city);

            return Ok(data);
        }
    }
}

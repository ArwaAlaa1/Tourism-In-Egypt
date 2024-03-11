using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism_Egypt.Controllers
{
   
    public class CityController : BaseApiController
    {
        private readonly TourismContext tourismContext;
        private readonly IGenericRepository<City> _cityRepo;
        private readonly IGenericRepository<CityPhotos> cityPhRepo;
        public static List<CityPhotoDTO> Convertion(List<CityPhotos> cityPhotos)
        {
            List<CityPhotoDTO> dtoList = new List<CityPhotoDTO>();

            foreach (var cityPhoto in cityPhotos)
            {
                CityPhotoDTO dto = new CityPhotoDTO();
                dto.Id= cityPhoto.Id;
                dto.Photo = cityPhoto.Photo;
              
                dtoList.Add(dto);
            }

            return dtoList;
        }
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
        public async Task<ActionResult<CityDTO>> GetCity(int id)
        {
            var city = await _cityRepo.GetAsync(id);
            var photos =  tourismContext.CityPhotos.Where(c => c.CityId == id).ToList();
            CityDTO map = new CityDTO();
           
            var mapped = new CityDTO()
            {
                Id = city.Id,
                Name = city.Name,
                Description = city.Description,
                Location = city.Location,
                cityPhotos = Convertion(photos),
                
                
            };
            return Ok(mapped);
        }
    }
}

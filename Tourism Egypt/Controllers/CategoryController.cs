using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Repository;

namespace Tourism_Egypt.Controllers
{

    public class CategoryController : BaseApiController
    {
  
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository,IMapper mapper)
        {
           
           _categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var data = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
           var category = await _categoryRepository.GetAsync(id);

            if (category == null)
                return NotFound();
           // var places = await _categoryRepository.GetAllPlacesBySpecificCategory(id);
           //var placemapped =mapper.Map<ICollection<Place>,ICollection<PlaceDTO>>(places);
            var data = mapper.Map<Category, CategoryDTO>(category);
            //data.Places = placemapped;
        

            return Ok(data);
        }
    }
}

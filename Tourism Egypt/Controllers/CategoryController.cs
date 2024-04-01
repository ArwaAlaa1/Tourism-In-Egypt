using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
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
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories(string? categoryName)
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(categoryName))
            {
                var results = categories.Where(e => e.Name.ToLower().Contains(categoryName.ToLower())).ToList();

                if (results.Count() == 0)
                    return NotFound("This Category Not Existing");
                else
                {
                   var placesearch = mapper.Map<IEnumerable<Category>, IEnumerable< CategoryDTO >>(results);
                return Ok(placesearch);
                }
                
            }
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

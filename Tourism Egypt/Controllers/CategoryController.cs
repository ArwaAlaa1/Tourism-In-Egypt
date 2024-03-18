using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{

    public class CategoryController : BaseApiController
    {

        public CategoryController(IGenericRepository<Category> categoryrepo)
        {
            _categoryrepo = categoryrepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var categories = await _categoryrepo.GetAllAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryrepo.GetAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }
    }
}
